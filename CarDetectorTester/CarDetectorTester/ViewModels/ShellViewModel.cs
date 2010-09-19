using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using CarDetectorTester.Models;
using CarDetectorTester.Views;

namespace CarDetectorTester.ViewModels
{
    public class ShellViewModel : Caliburn.Micro.PropertyChangedBase
    {
        private CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        private log4net.ILog _logger;
        private Task frequencyQueryWorker;
        private object _updateRealtimeDataLock = new object();

        private const string hexFormat = "{0:x2} ";
        private const string decFormat = "{0:d2} ";

        Stream stream = null;
        BinaryReader reader = null;
        BinaryWriter writer = null;
        SerialPort serialPort = null;

        public Models.ChannelStatistics Channel1Stat { get; set; }
        public Models.ChannelStatistics Channel2Stat { get; set; }


        private int _carSpeedCh1;
        public int CarSpeedCh1
        {
            get { return _carSpeedCh1; }
            set
            {
                _carSpeedCh1 = value;
                NotifyOfPropertyChange(() => CarSpeedCh1);
            }
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                NotifyOfPropertyChange(() => IsRunning);
                NotifyOfPropertyChange(() => CanStart);
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
                NotifyOfPropertyChange(()=>CanUpdateRealtimeData);
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
                NotifyOfPropertyChange(() => CommandName);
            }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyOfPropertyChange(() => Status);
            }
        }

        private string _comPort = "com1";
        public string ComPort
        {
            get { return _comPort; }
            set
            {
                _comPort = value;
                NotifyOfPropertyChange(() => ComPort);
                NotifyOfPropertyChange(() => CanStart);
            }
        }

        private int _baundRate = 9600;
        public int BaundRate
        {
            get { return _baundRate; }
            set
            {
                _baundRate = value;
                NotifyOfPropertyChange(() => BaundRate);
                NotifyOfPropertyChange(() => CanStart);
            }
        }

        private string _commandToSend ;
        public string CommandToSend
        {
            get { return _commandToSend; }
            set
            {
                _commandToSend = value;
                NotifyOfPropertyChange(() => CommandToSend);
                NotifyOfPropertyChange(() => CanStart);
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
                NotifyOfPropertyChange(()=>CanSendCmd);
            }
        }

        public ObservableCollection<Models.ResponseData> Responses { get; set; }
        public ObservableCollection<Models.ResponseData> Responses1 { get; set; }
        public ObservableCollection<Models.ResponseData> Responses2 { get; set; }


        public ShellViewModel()
        {
            Responses = new ObservableCollection<ResponseData>();
            Responses1 = new ObservableCollection<ResponseData>();
            Responses2 = new ObservableCollection<ResponseData>();

            Channel1Stat = new Models.ChannelStatistics() { ChannelName = "1通道" };
            Channel2Stat = new Models.ChannelStatistics() { ChannelName = "2通道" };

            CommandToSend = Properties.Settings.Default.LastCommand;


            _commandName = "开始";
            _logger = log4net.LogManager.GetLogger(typeof(ShellViewModel));

        }

        public void Connect()
        {
            try
            {
#if DEBUG
            stream = new StreamMock();
#else
                _logger.Info(_comPort);
                _logger.Info(_baundRate);


                serialPort = new SerialPort(_comPort);
                serialPort.BaudRate = _baundRate;
                serialPort.Parity = Parity.None;
                serialPort.StopBits = StopBits.One;
                serialPort.DataBits = 8;
                serialPort.DtrEnable = true;
                serialPort.Open();
                stream = serialPort.BaseStream;
#endif
                CanSendCmd = true;
                CanUpdateRealtimeData = true;
                CanSetRect = true;

                RunRecv();

            }
            catch (Exception ex)
            {
                Execute.OnUIThread(()=> MessageBox.Show(ex.Message) );
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
                NotifyOfPropertyChange(()=>CanSetRect);
            }
        }

        public void SetRect()
        {
            var form = new WidthAndHeight();
            var result = form.ShowDialog();

            if (result.HasValue && result.Value == true)
            {
                var l = form.RectLength;
                var w = form.RectWidth;

                var cmd = string.Format(Properties.Settings.Default.SetLongAndWidthCommand, l, w);
                SendHexCommand(cmd);
            }
        }


        public void SendCmd()
        {
#if DEBUG
            Channel1Stat.CarInCount+=1;
            Channel1Stat.CarOutCount+=2;
            Channel1Stat.IsCarIn = !Channel1Stat.IsCarIn;

            Channel2Stat.CarInCount+=3;
            Channel2Stat.CarOutCount+=4;
            Channel2Stat.IsCarIn = !Channel2Stat.IsCarIn;

            CarSpeedCh1 = DateTime.Now.Millisecond;
            return;
#endif

            var rawData = "";

            var hexData = Converter.StringToByteArray(CommandToSend.Replace(" ", ""));
            writer.Write(hexData);
            serialPort.BaseStream.Flush();


        }

        public void RunRecv()
        {
            if (IsRunning)
            {
                if (serialPort != null)
                {
                    serialPort.Dispose();
                }

                _cancelTokenSource.Cancel();
                return;
            }


            var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            _cancelTokenSource = new CancellationTokenSource();

            var worker = Task.Factory.StartNew(() =>
                                                   {
                                                       Execute.OnUIThread(() =>
                                                                              {
                                                                                  IsRunning = true;
                                                                                  CommandName = "停止";
                                                                              });

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

                                                           reader = new BinaryReader(stream);
                                                           writer = new BinaryWriter(stream);

                                                          
                                                           while (true)
                                                           {
                                                               if (_cancelTokenSource.Token.IsCancellationRequested)
                                                               {
                                                                   break;
                                                               }

                                                               var hexResponse = new byte[4];
                                                               //skip header
                                                               ReadLength(reader, hexResponse, 0, 2);
                                                               if ((hexResponse[0] != 0x55)||(hexResponse[1] != 0xaa))
                                                               {
                                                                   if (serialPort != null)
                                                                   {
                                                                        serialPort.BaseStream.Flush();
                                                                   }
#if DEBUG
                                                                   Thread.Sleep(1000);
#endif

                                                                   continue;
                                                               }

                                                               ReadLength(reader, hexResponse, 0, 2);
                                                               var length = hexResponse[0]-1;
                                                               var commandType = hexResponse[1];

                                                               var packet = new byte[length];
                                                               ReadLength(reader, packet, 0, packet.Length);


                                                               switch (commandType)
                                                               {
                                                                       //car in,out
                                                                   case 0x11:
                                                                   case 0x10:
                                                                       HandleCarInOut(commandType, packet);
                                                                       break;
                                                                       //speed
                                                                   case 0x12:
                                                                       HandleSpeed(packet);
                                                                       break;
                                                                       //frequency
                                                                   case 0x02:
                                                                       HandleFrequency(packet);
                                                                       break;
                                                               }
                                                           }

#if DEBUG
                                                           Thread.Sleep(1000);
#endif

                                                       }
                                                       finally
                                                       {
                                                           if (reader != null)
                                                           {
                                                               reader.Dispose();
                                                           }
                                                           if (writer != null)
                                                           {
                                                               writer.Dispose();
                                                           }

                                                           if (stream != null)
                                                           {
                                                               stream.Dispose();
                                                           }

                                                           if (serialPort != null)
                                                           {
                                                               serialPort.Dispose();
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

            worker.ContinueWith(result => MessageBox.Show(result.Exception.InnerExceptions[0].Message+Environment.NewLine + result.Exception.ToString(), "", MessageBoxButton.OK, MessageBoxImage.Error ),
                    CancellationToken.None,
                    TaskContinuationOptions.OnlyOnFaulted,
                    taskScheduler
                    );
        }

        private void SendHexCommand(string cmdString)
        {
            var hexData = Converter.StringToByteArray(cmdString.Replace(" ", ""));
            writer.Write(hexData);
        }

        private void HandleSpeed(byte[] packet)
        {
            int speed = packet[0];
            speed = speed << 8;
            speed += packet[1];

            Execute.OnUIThread(() => CarSpeedCh1 = speed);
            
        }

        private void HandleCarInOut(byte command, byte[] packet)
        {
            if (command == 0x10)//in
            {
                Execute.OnUIThread( ()=>
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
                                        });
            }
        }

        private void HandleFrequency(byte[] packet)
        {
            var rawData = "";

            rawData += Converter.ByteArrayToString(packet, hexFormat);

            var responseGroup = new List<ResponseData>();
            var br = new BinaryReader(new MemoryStream(packet));

            for (int i = 0; i < 4; i++)
            {
                var dataBuffer = new byte[2];

                var data1 = ReadChannelData(br,
                                            dataBuffer, "{0:d2} ");

                rawData += Converter.ByteArrayToString(dataBuffer,
                                                       hexFormat);

                var data2 = ReadChannelData(br,
                                            dataBuffer, "{0:d2} ");

                rawData += Converter.ByteArrayToString(dataBuffer,
                                                       hexFormat);


                var responseData = new ResponseData
                                       {
                                           ChannelId = (i + 1).ToString(),
                                           Data1 = data1,
                                           Data2 = data2,
                                           Data3 = (int.Parse(data1) * 25000.0 / int.Parse(data2)).ToString("f3"),
                                       };

                responseGroup.Add(responseData);
            }
            //skip checksum
            var checkSum = br.ReadByte();
            rawData += checkSum.ToString("x2");

            _logger.Info(rawData);

            responseGroup.Reverse();
            foreach (var responseData in responseGroup)
            {
                var copy = responseData;
                Execute.OnUIThread(() =>
                                       {
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
                    );
            }

            Execute.OnUIThread(() =>
                                   {
                                       Responses.Insert(0,
                                                        new ResponseData
                                                            {
                                                                Time
                                                                    =
                                                                    null
                                                            });
                                       RemoveOldData();
                                   });

        }

        private void RemoveOldData()
        {
            if (Responses1.Count > 20)
            {
                Responses1.RemoveAt(0);
            }

            if (Responses2.Count > 20)
            {
                Responses2.RemoveAt(0);
            }
        }

        private void ReadLength(BinaryReader rdr, byte[] hexResponse, int offset, int length)
        {
            var count = 0;
            var readAttemp = 0;
            while (count < length)
            {
                var readCount = rdr.Read(hexResponse, offset + count, length - count);
                count += readCount;
            }
        }

        private string ReadChannelData(BinaryReader binaryReader, byte[] dataBuffer, string formatString)
        {
            ReadLength(binaryReader, dataBuffer, 0, dataBuffer.Length);

            var netData = BitConverter.ToUInt16(dataBuffer, 0);
            var hostData = (ushort)System.Net.IPAddress.NetworkToHostOrder((short)netData);

            return hostData.ToString();
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
