using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using mCore;
using RBSPAdapter_COM;
using mCore;

namespace TestForm
{
    public partial class Form1 : Form
    {
        private const string NAME = "Demo";
        private mCore.SMS _sms;
        private System.Collections.Concurrent.ConcurrentQueue<string> _quries = new ConcurrentQueue<string>();

        public Form1()
        {
            InitializeComponent();
            Func();

            if (_sms == null)
            {
                _sms = new SMS();
                _sms.Port = "com16";
                _sms.Encoding = Encoding.Unicode_16Bit;
                _sms.BaudRate = BaudRate.BaudRate_9600;
                _sms.DataBits = DataBits.Eight;
                _sms.Parity = Parity.None;
                _sms.StopBits = StopBits.One;
                _sms.FlowControl = FlowControl.None;
                _sms.NewMessageConcatenate = true;
                _sms.NewMessageIndication = true;
                _sms.AutoDeleteNewMessage = true;

                _sms.NewMessageReceived += _sms_NewMessageReceived;
            }

            new Thread(Server).Start(null);
        }

        void _sms_NewMessageReceived(object sender, mCore.NewMessageReceivedEventArgs e)
        {
            _quries.Enqueue(e.TextMessage);
        }


        void Server(object idNo)
        {
            var binaryFormatter = new BinaryFormatter();
            using (NamedPipeClientStream client = new NamedPipeClientStream(".", NAME, PipeDirection.InOut))
            {
                client.Connect();

                using (StreamWriter sw = new StreamWriter(client))
                {
                    using (StreamReader sr = new StreamReader(client))
                    {
                        while (true)
                        {
                            string query = null;
                            if (_quries.TryDequeue(out query))
                            {
                                sw.WriteLine(query);
                                sw.Flush();
                                string s = (string) binaryFormatter.Deserialize(client);
                                System.Diagnostics.Debug.WriteLine("=========: \r\n" + s + "\r\n");
                                //_sms.SendSMS("13547962367", s.Substring(0, 10));
                            }
                            else
                            {
                                System.Threading.Thread.Sleep(3000);
                            }
                        }
                    }
                }
            }



        }

        private static void WriteGreen(string p)
        {
            System.Diagnostics.Debug.WriteLine(p);
        }

        private static void WriteGreen(string p, int i, string s)
        {
            System.Diagnostics.Debug.WriteLine(p + i.ToString() + s);
        }


        static void Func()
        {

        }
    }
}
