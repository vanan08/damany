using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CarDetectorTester.Models;
using CarDetectorTester.Views;
using Ciloci.Flee;
using MEFedMVVM.Services.Contracts;
using MEFedMVVM.ViewModelLocator;
using MiscUtil.Conversion;
using MiscUtil.IO;
using Cinch;

namespace CarDetectorTester.ViewModels
{
    [ExportViewModel("ShellViewModel")]
    public class ShellViewModel : Cinch.EditableValidatingViewModelBase
    {
        private readonly IMessageBoxService _messageBoxService;
        private readonly IUIVisualizerService _uiVisualizerService;
        private readonly IViewAwareStatusWindow _viewStatusWindow;
        private CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        private log4net.ILog _logger;
        private Task frequencyQueryWorker;
        private object _updateRealtimeDataLock = new object();
        private EndianBitConverter _endianConverter = EndianBitConverter.Big;

        private IGenericExpression<double> _expression;
        private ExpressionContext _context;

        private const string hexFormat = "{0:x2} ";
        private const string decFormat = "{0:d2} ";

        Stream _stream = null;
        EndianBinaryReader _reader = null;
        EndianBinaryWriter _writer = null;
        SerialPort _serialPort = null;

        public Models.ChannelStatistics Channel1Stat { get; set; }
        public Models.ChannelStatistics Channel2Stat { get; set; }

        public SimpleCommand<object, object> SetLengthAndWidthCommand { get; private set; }


        private int _carSpeedCh1;
        public int CarSpeedCh1
        {
            get { return _carSpeedCh1; }
            set
            {
                _carSpeedCh1 = value;
                NotifyPropertyChanged("CarSpeedCh1");
            }
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                NotifyPropertyChanged("IsRunning");
                NotifyPropertyChanged("CanStart");
            }
        }


        private bool _canUpdateRealtimeData=true;
        public bool CanUpdateRealtimeData
        {
            get
            {
                return _canUpdateRealtimeData;
            }
            set
            {
                _canUpdateRealtimeData = value;
                NotifyPropertyChanged("CanUpdateRealtimeData");
            }
        }

        private bool _updateRealtimeData;
        public bool UpdateRealtimeData
        {
            get
            {
                lock (_updateRealtimeDataLock)
                {
                    return _updateRealtimeData;
                }
                
            }
            set
            {
                lock (_updateRealtimeDataLock)
                {
                    _updateRealtimeData = value;
                }
                
            }
        }


        private string _commandName;
        public string CommandName
        {
            get { return _commandName; }
            set
            {
                _commandName = value;
                NotifyPropertyChanged("CommandName");
            }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }

        private string _comPort = "com1";
        public string ComPort
        {
            get { return _comPort; }
            set
            {
                _comPort = value;
                NotifyPropertyChanged("ComPort");
                NotifyPropertyChanged("CanStart");
            }
        }

        private int _baundRate = 9600;
        public int BaundRate
        {
            get { return _baundRate; }
            set
            {
                _baundRate = value;
                NotifyPropertyChanged("BaundRate");
                NotifyPropertyChanged("CanStart");
            }
        }

        private string _commandToSend ;
        public string CommandToSend
        {
            get { return _commandToSend; }
            set
            {
                _commandToSend = value;
                NotifyPropertyChanged("CommandToSend");
                NotifyPropertyChanged("CanStart");
            }
        }


        private bool _canSendCmd;
        public bool CanSendCmd
        {
            get
            {
                return _canSendCmd;
            }
            set
            {
                _canSendCmd = value;
                NotifyPropertyChanged("CanSendCmd");
            }
        }

        private bool _reportSpeed = false;
        public bool ReportSpeed
        {
            get { return _reportSpeed; }
            set
            {
                _reportSpeed = value;
                NotifyPropertyChanged("ReportSpeed");
                if (_reportSpeed)
                {
                   SendHexCommand(Properties.Settings.Default.EnableSpeedReportCommand); 
                }
                else
                {
                    SendHexCommand(Properties.Settings.Default.DisableSpeedReportCommand);
                }
            }
        }


        private bool _useDfaProtocol=true;
        public bool UseDfaProtocol
        {
            get { return _useDfaProtocol; }
            set
            {
                _useDfaProtocol = value;
                NotifyPropertyChanged("UseDfaProtocol");
                if (_useDfaProtocol)
                {
                    SendHexCommand(Properties.Settings.Default.EnableDFAProtocol);
                }
                else
                {
                    SendHexCommand(Properties.Settings.Default.DisableDFAProtocol);
                }
            }
        }

        public DispatcherNotifiedObservableCollection<Models.ResponseData> Responses { get; set; }
        public DispatcherNotifiedObservableCollection<Models.ResponseData> Responses1 { get; set; }
        public DispatcherNotifiedObservableCollection<Models.ResponseData> Responses2 { get; set; }

        [ImportingConstructor]
        public ShellViewModel(IMessageBoxService messageBoxService,
                              IUIVisualizerService uiVisualizerService,
                              IViewAwareStatusWindow viewStatusWindow)
        {
            _messageBoxService = messageBoxService;
            _uiVisualizerService = uiVisualizerService;
            _viewStatusWindow = viewStatusWindow;

            Responses = new DispatcherNotifiedObservableCollection<ResponseData>();
            Responses1 = new DispatcherNotifiedObservableCollection<ResponseData>();
            Responses2 = new DispatcherNotifiedObservableCollection<ResponseData>();

            Channel1Stat = new Models.ChannelStatistics() { ChannelName = "1通道" };
            Channel2Stat = new Models.ChannelStatistics() { ChannelName = "2通道" };

            CommandToSend = Properties.Settings.Default.LastCommand;


            _commandName = "开始";
            _logger = log4net.LogManager.GetLogger(typeof(ShellViewModel));

            _context = new ExpressionContext();
            _context.Imports.AddType(typeof (Math));

            _context.Variables["data1"] = 0.0;
            _context.Variables["data2"] = 0.0;

            _expression = _context.CompileGeneric<double>(Properties.Settings.Default.Data3Expression);

            SetLengthAndWidthCommand = new SimpleCommand<object, object>(x=>SetRect());

            _viewStatusWindow.ViewWindowClosing += _viewStatusWindow_ViewWindowClosing;

        }

        void _viewStatusWindow_ViewWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.LastCommand = CommandToSend;
            Properties.Settings.Default.Save();
        }


        public void Connect()
        {
            try
            {
                _logger.Info(_comPort);
                _logger.Info(_baundRate);

                _serialPort = new SerialPort(_comPort);
                _serialPort.BaudRate = _baundRate;
                _serialPort.Parity = Parity.None;
                _serialPort.StopBits = StopBits.One;
                _serialPort.DataBits = 8;
                _serialPort.DtrEnable = true;
                _serialPort.Open();
                _stream = _serialPort.BaseStream;

                CanSendCmd = true;
                CanUpdateRealtimeData = true;
                CanSetRect = true;

                RunRecv();

            }
            catch (Exception ex)
            {
                _messageBoxService.ShowError(ex.Message);
            }
        }

        public void ResetStatistics()
        {
            Channel1Stat.Reset();
            Channel2Stat.Reset();

            CarSpeedCh1 = 0;
        }

        private bool _canSetRect;
        public bool CanSetRect
        {
            get { return _canSetRect; }
            set
            { 
                _canSetRect = value;
                NotifyPropertyChanged("CanSetRect");
            }
        }

        public void SetRect()
        {
            var size = new Models.LengthAndWidth();
            var result = _uiVisualizerService.ShowDialog("WidthAndHeightPopup", size);

            if (result.HasValue && result.Value == true)
            {
                var cmd = string.Format(Properties.Settings.Default.SetLongAndWidthCommand, size.Length, size.Width);
                SendHexCommand(cmd);
            }
        }


        public void SendCmd()
        {
            var rawData = "";

            var hexData = Converter.StringToByteArray(CommandToSend.Replace(" ", ""));
            _writer.Write(hexData);
            _serialPort.BaseStream.Flush();
        }

        public void RunRecv()
        {
            if (IsRunning)
            {
                if (_serialPort != null)
                {
                    _serialPort.Dispose();
                }

                _cancelTokenSource.Cancel();
                return;
            }


            var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            _cancelTokenSource = new CancellationTokenSource();

            var worker = Task.Factory.StartNew(() =>
                                                   {
                                                       IsRunning = true;
                                                       CommandName = "停止";

                                                       Task.Factory.StartNew(() =>
                                                       {
                                                           while (true)
                                                           {
                                                               if (UpdateRealtimeData)
                                                               {
                                                                   SendHexCommand(Properties.Settings.Default.FrequencyQueryCmd);
                                                               }
                                                              
                                                               Thread.Sleep(Properties.Settings.Default.FrequencyQueryIntervalInMs);
                                                           }
                                                       });


                                                       try
                                                       {

                                                           _reader = new EndianBinaryReader(_endianConverter, _stream);
                                                           _writer = new EndianBinaryWriter(_endianConverter, _stream);

                                                           while (true)
                                                           {
                                                               if (_cancelTokenSource.Token.IsCancellationRequested)
                                                               {
                                                                   break;
                                                               }

                                                               //skip header
                                                               var flag55 = _reader.ReadByte();
                                                               if ( flag55 != 0x55) continue;
                                                               
                                                               var flagAA = _reader.ReadByte();
                                                               if (flagAA != 0xaa) continue;
                                                               
                                                               var length = _reader.ReadByte();
                                                               if (length > Properties.Settings.Default.MaxPackLength)
                                                               {
                                                                   continue;
                                                               }

                                                               var packet  = _reader.ReadBytes(length);

                                                               var commandType = packet[0];

                                                               var payLoad = new byte[packet.Length - 1];
                                                               Array.Copy(packet, 1, payLoad, 0, payLoad.Length);

                                                               switch (commandType)
                                                               {
                                                                       //car in,out
                                                                   case 0x11:
                                                                   case 0x10:
                                                                       HandleCarInOut(commandType, payLoad);
                                                                       break;
                                                                       //speed
                                                                   case 0x12:
                                                                       HandleSpeed(payLoad);
                                                                       break;
                                                                       //frequency
                                                                   case 0x02:
                                                                       HandleFrequency(payLoad);
                                                                       break;
                                                               }
                                                           }

#if DEBUG
                                                           Thread.Sleep(1000);
#endif

                                                       }
                                                       finally
                                                       {
                                                           if (_reader != null)
                                                           {
                                                               _reader.Dispose();
                                                           }
                                                           if (_writer != null)
                                                           {
                                                               _writer.Dispose();
                                                           }

                                                           if (_stream != null)
                                                           {
                                                               _stream.Dispose();
                                                           }

                                                           if (_serialPort != null)
                                                           {
                                                               _serialPort.Dispose();
                                                           }

                                                           IsRunning = false;
                                                       }
                                                   }, _cancelTokenSource.Token);

                worker.ContinueWith(
                result =>
                {
                    IsRunning = false;
                    CommandName = "开始";
                },
                CancellationToken.None,
                TaskContinuationOptions.None,
                taskScheduler);

            worker.ContinueWith(result => _messageBoxService.ShowError(result.Exception.InnerExceptions[0].Message),
                    CancellationToken.None,
                    TaskContinuationOptions.OnlyOnFaulted,
                    taskScheduler
                    );
        }

        private void SendHexCommand(string cmdString)
        {
            try
            {
                var hexData = Converter.StringToByteArray(cmdString.Replace(" ", ""));
                _writer.Write(hexData);
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowError(ex.Message);
            }
            
        }

        private void HandleSpeed(byte[] packet)
        {
            var r = new EndianBinaryReader(_endianConverter, new MemoryStream(packet));
            var speed = r.ReadInt16();

             CarSpeedCh1 = speed;
            
        }

        private void HandleCarInOut(byte command, byte[] packet)
        {
            if (command == 0x10)//in
            {
                var ch1 = packet[0] & 0x03;
                if (ch1==2)
                {
                    Channel1Stat.CarInCount++;
                    Channel1Stat.IsCarIn = true;
                }
                else if (ch1 == 1)
                {
                    Channel1Stat.CarOutCount++;
                    Channel1Stat.IsCarIn = false;
                }

                var ch2 = (packet[0]>>2) & 0x03;
                if (ch2 == 2)
                {
                    Channel2Stat.CarInCount++;
                    Channel2Stat.IsCarIn = true;
                }
                else if (ch2 == 1)
                {
                    Channel2Stat.CarOutCount++;
                    Channel2Stat.IsCarIn = false;
                }
                
            }
        }



        private void HandleFrequency(byte[] packet)
        {
            var rawData = "";

            rawData += Converter.ByteArrayToString(packet, hexFormat);

            var responseGroup = new List<ResponseData>();
            var reader = new EndianBinaryReader(_endianConverter, new MemoryStream(packet));

            for (int i = 0; i < 4; i++)
            {

                var data1 = reader.ReadUInt16();
                var data2 = reader.ReadUInt16();

                _context.Variables["data1"] = (double)data1;
                _context.Variables["data2"] = (double) data2;

                var data3 = _expression.Evaluate();

                var responseData = new ResponseData
                                       {
                                           ChannelId = (i + 1).ToString(),
                                           Data1 = data1.ToString("d2"),
                                           Data2 = data2.ToString("d2"),
                                           Data3 = data3.ToString("f3"),
                                       };

                responseGroup.Add(responseData);
            }
            //skip checksum
            var checkSum = reader.ReadByte();
            rawData += checkSum.ToString("x2");

            _logger.Info(rawData);

            responseGroup.Reverse();
            foreach (var responseData in responseGroup)
            {
                var copy = responseData;
                Responses.Insert(0, copy);
                if (copy.ChannelId == "1")
                {
                    Responses1.Add(copy);
                }
                if (copy.ChannelId == "2")
                {
                    Responses2.Add(copy);
                }
                RemoveOldData();
                    
            }

                                   
            Responses.Insert(0, new ResponseData{Time = null});
            RemoveOldData();
                                  

        }

        private void RemoveOldData()
        {
            if (Responses.Count >20)
            {
                Responses.RemoveAt(Responses.Count-1);
            }

            if (Responses1.Count > 20)
            {
                Responses1.RemoveAt(0);
            }

            if (Responses2.Count > 20)
            {
                Responses2.RemoveAt(0);
            }
        }

     

        public bool CanStart
        {
            get
            {

                if (string.IsNullOrEmpty(ComPort) || !ComPort.ToUpper().StartsWith("COM"))
                {
                    return false;
                }

                if (BaundRate == 0)
                {
                    return false;
                }

                if (string.IsNullOrEmpty(CommandToSend))
                {
                    return false;
                }

                return true;
            }
        }
    }
}
