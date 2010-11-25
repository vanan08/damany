using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CarDetectorTester.Models;
using CarDetectorTester.Views;
using Ciloci.Flee;
using MEFedMVVM.Services.Contracts;
using MEFedMVVM.ViewModelLocator;
using MiscUtil.Conversion;
using MiscUtil.IO;
using Cinch;
using System.Linq;

namespace CarDetectorTester.ViewModels
{
    [ExportViewModel("ShellViewModel")]
    public class ShellViewModel : Cinch.EditableValidatingViewModelBase
    {
        private const string OpenText = "打开";
        private const string CloseText = "关闭";

        private bool _connectCommandEnabled = true;
        public bool ConnectCommandEnabled
        {
            get { return _connectCommandEnabled; }
            set
            {
                _connectCommandEnabled = value; 
                NotifyPropertyChanged("ConnectCommandEnabled");
            }
        }

        private readonly object _isClosingLocker = new object();
        private bool _isClosing = false;
        bool IsClosing
        {
            get
            {
                lock (_isClosingLocker)
                {
                    return _isClosing;
                }
                
            }
            set
            {
                lock (_isClosingLocker)
                {
                    _isClosing = value;
                }
            }
        }


        private readonly IMessageBoxService _messageBoxService;
        private readonly IUIVisualizerService _uiVisualizerService;
        private readonly IViewAwareStatusWindow _viewStatusWindow;


        private CancellationTokenSource _cancelTokenSource;
        private log4net.ILog _logger;
        private Task frequencyQueryWorker;

        private IGenericExpression<double> _expression;
        private ExpressionContext _context;

        private const string hexFormat = "{0:x2} ";
        private const string decFormat = "{0:d2} ";

        Stream _stream = null;
        private EndianBitConverter _endianConverter = EndianBitConverter.Big;
        EndianBinaryReader _reader = null;
        EndianBinaryWriter _writer = null;
        SerialPort _serialPort = null;

        private Task _receiver;
        private Task _sender;

        public Models.ChannelStatistics Channel1Stat { get; private set; }
        public Models.ChannelStatistics Channel2Stat { get; private set; }

        public SimpleCommand<object, object> SetLengthAndWidthCommand { get; private set; }
        public SimpleCommand<object, object> OpenSerialportCommand { get; private set; }


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
            }
        }


        private bool _canUpdateRealtimeData = true;
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


        private object _updateRealtimeDataLock = new object();
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


        private string _buttonDisplayText;
        public string ButtonDisplayText
        {
            get { return _buttonDisplayText; }
            set
            {
                _buttonDisplayText = value;
                NotifyPropertyChanged("ButtonDisplayText");
            }
        }


        public SerialportConfig SerialportConf { get; private set; }

        private string _commandToSend;
        public string CommandToSend
        {
            get { return _commandToSend; }
            set
            {
                _commandToSend = value;
                NotifyPropertyChanged("CommandToSend");
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


        private bool _useDfaProtocol = true;
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

            SerialportConf = new SerialportConfig()
                                   {
                                       BaundRate = 9600,
                                       PortName = "com1",
                                   };

            Channel1Stat = new Models.ChannelStatistics() { ChannelName = "1通道" };
            Channel2Stat = new Models.ChannelStatistics() { ChannelName = "2通道" };

            CommandToSend = Properties.Settings.Default.LastCommand;
            _buttonDisplayText = OpenText;

            SetLengthAndWidthCommand = new SimpleCommand<object, object>(x => SetRect());
            OpenSerialportCommand = new SimpleCommand<object, object>(x=> ConnectCommandEnabled, Connect);

            _logger = log4net.LogManager.GetLogger(typeof(ShellViewModel));

            CompileExpression();

            _viewStatusWindow.ViewWindowClosing += _viewStatusWindow_ViewWindowClosing;
        }

        private void CompileExpression()
        {
            _context = new ExpressionContext();
            _context.Imports.AddType(typeof(Math));

            _context.Variables["data1"] = 0.0;
            _context.Variables["data2"] = 0.0;

            _expression = _context.CompileGeneric<double>(Properties.Settings.Default.Data3Expression);
        }

        private void Connect(object x)
        {
            
            try
            {
                if (ButtonDisplayText == OpenText)
                {
                    OpenSerialport();

                    _cancelTokenSource = new CancellationTokenSource();
                    IsClosing = false;

                    StartRecvWorker();
                    StartSendWorker();

                    EnableCommand(true);
                    ButtonDisplayText = CloseText;
                }
                else if (ButtonDisplayText == CloseText)
                {
                    ConnectCommandEnabled = false;
                    
                    CloseSerialport();
                    
                    IsClosing = true;
                    _cancelTokenSource.Cancel();

                   

                    Task.Factory.ContinueWhenAll(new[] { _sender, _receiver }, ants =>
                                            {
                                                AggregateException exception = null;
                                                Array.ForEach(ants, ant =>
                                                                        {
                                                                            exception = ant.Exception;
                                                                        });
                                                
                                                ConnectCommandEnabled = true;

                                                Action doIt = () => CommandManager.InvalidateRequerySuggested();
                                                
                                                (_viewStatusWindow.View as DependencyObject).Dispatcher.BeginInvoke(doIt);
                                               
                                            });

                    EnableCommand(false);
                    ButtonDisplayText = OpenText;
                }
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowError(ex.Message);
            }
        }


        void _viewStatusWindow_ViewWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CloseSerialport();

            Properties.Settings.Default.LastCommand = CommandToSend;
            Properties.Settings.Default.Save();
        }


        public void OpenSerialport()
        {
            _logger.Info(this.SerialportConf.PortName);
            _logger.Info(this.SerialportConf.BaundRate);

            _serialPort = new SerialPort(this.SerialportConf.PortName);
            _serialPort.BaudRate = this.SerialportConf.BaundRate;
            _serialPort.Parity = Parity.None;
            _serialPort.StopBits = StopBits.One;
            _serialPort.DataBits = 8;
            _serialPort.DtrEnable = true;
            _serialPort.Open();
            _stream = _serialPort.BaseStream;
        }

        private void EnableCommand(bool enable)
        {
            CanSendCmd = enable;
            CanUpdateRealtimeData = enable;
            CanSetRect = enable;
        }

        void CloseSerialport()
        {

            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Close();
                _serialPort.Dispose();
            }

            if (_observer != null)
            {
                _observer.Dispose();
            }

            EnableCommand(false);
            ButtonDisplayText = OpenText;
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

        private IDisposable _observer;

        public void StartRecvWorker()
        {

            _receiver = Task.Factory.StartNew(() =>
                                                   {
                                                       IsRunning = true;

                                                       try
                                                       {

                                                           _reader = new EndianBinaryReader(_endianConverter, _stream);
                                                           _writer = new EndianBinaryWriter(_endianConverter, _stream);

                                                           while (true)
                                                           {
                                                               _cancelTokenSource.Token.ThrowIfCancellationRequested();

                                                               //skip header
                                                               var flag55 = _reader.ReadByte();
                                                               if (flag55 != 0x55) continue;

                                                               var flagAA = _reader.ReadByte();
                                                               if (flagAA != 0xaa) continue;

                                                               var length = _reader.ReadByte();
                                                               if (length > Properties.Settings.Default.MaxPackLength)
                                                               {
                                                                   continue;
                                                               }

                                                               var packet = _reader.ReadBytes(length);

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
                                                           IsRunning = false;
                                                       }
                                                   }, _cancelTokenSource.Token);

            //worker.ContinueWith(ant =>
            //                        {
            //                            if (IsClosing)
            //                            {
            //                                var ignore = ant.Exception;
            //                            }
            //                            else 
            //                                _messageBoxService.ShowError(ant.Exception.InnerException.Message + _isClosing);
            //                        }, TaskContinuationOptions.OnlyOnFaulted);
        }

        private byte[] ReadExistingBytes(IEvent<SerialDataReceivedEventArgs> evt)
        {
            var serial = (SerialPort)evt.Sender;
            var buffer = new byte[serial.BytesToRead];
            serial.Read(buffer, 0, buffer.Length);
            return buffer;
        }

        private void StartSendWorker()
        {
           _sender = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    _cancelTokenSource.Token.ThrowIfCancellationRequested();

                    if (UpdateRealtimeData)
                        SendHexCommand(Properties.Settings.Default.FrequencyQueryCmd);

                    Thread.Sleep(Properties.Settings.Default.FrequencyQueryIntervalInMs);
                }
            }, _cancelTokenSource.Token);

           //sender.ContinueWith(ant =>
           //                        {
           //                            if (IsClosing)
           //                            {
           //                                var ignore = ant.Exception;
           //                            }
           //                            else
           //                                _messageBoxService.ShowError(ant.Exception.InnerException.Message+_isClosing);
           //                        }, TaskContinuationOptions.OnlyOnFaulted);

        }

        private void SendHexCommand(string cmdString)
        {
            var hexData = Converter.StringToByteArray(cmdString.Replace(" ", ""));
            _writer.Write(hexData);
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
                if (ch1 == 2)
                {
                    Channel1Stat.CarInCount++;
                    Channel1Stat.IsCarIn = true;
                }
                else if (ch1 == 1)
                {
                    Channel1Stat.CarOutCount++;
                    Channel1Stat.IsCarIn = false;
                }

                var ch2 = (packet[0] >> 2) & 0x03;
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
                _context.Variables["data2"] = (double)data2;

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

            Responses.Insert(0, new ResponseData { Time = null });
            RemoveOldData();

        }

        private void RemoveOldData()
        {
            if (Responses.Count > 20)
            {
                Responses.RemoveAt(Responses.Count - 1);
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


    }
}
