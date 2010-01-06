using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using Damany.Net;
using System.Net.Sockets;
using System.Threading;

namespace Damany.Component
{
    public partial class SanyoNetCamera : System.ComponentModel.Component, ICamera, IDisposable
    {
        const string _ShutterMode = "shutter_sw";
        const string _ShutterShortSpeedLevel = "short_speed";
        const string _ShutterLongSpeedLevel = "long_speed";

        const string _IrisMode = "iris_sw";
        const string _IrisManualLevel = "manu_level";
        const string _IrisAutoLevel = "auto_level";

        const string _AgcMode = "agc_sw";
        const string _DigitalGain = "digital_gain";

        public SanyoNetCamera()
        {
            InitializeComponent();
        }

        private string BuildUri(KeyValuePair<string, object>[] nameValues)
        {

            var sb = new StringBuilder();
            sb.AppendFormat("http://{0}/cgi-bin/camera_quality.cgi?", this.IPAddress);

            foreach (var name in nameValues)
            {
                sb.AppendFormat("{0}={1}&", name.Key, name.Value);
            }


            return sb.ToString();
        }

        public void SetShutterSpeed(ShutterMode mode, int speedLevel)
        {
            int modeValue = 0;
            string speedPropertyName = null;
            switch (mode)
            {
                case ShutterMode.Off:
                    modeValue = 0;
                    speedPropertyName = "dummy";
                    break;
                case ShutterMode.Short:
                    modeValue = 1;
                    speedPropertyName = _ShutterShortSpeedLevel;
                    break;
                case ShutterMode.Long:
                    modeValue = 2;
                    speedPropertyName = _ShutterLongSpeedLevel;
                    break;
                default:
                    throw new InvalidEnumArgumentException("mode");
                    break;
            }

            
            string uri = BuildUri( new KeyValuePair<string, object>[]
                                   {
                                       new KeyValuePair<string, object>(_ShutterMode, modeValue),
                                       new KeyValuePair<string, object>(speedPropertyName, speedLevel)
                                   });

            SetCameraProperty(uri);
        }

        public void SetIrisLevel(IrisMode mode, int level)
        {
            int irisModeValue = 0;
            string propertyName = null;
            switch (mode)
            {
                case IrisMode.Auto:
                    irisModeValue = 0;
                    propertyName = _IrisAutoLevel;
                    break;
                case IrisMode.Manual:
                    irisModeValue = 1;
                    propertyName = _IrisManualLevel;
                    break;
                default:
                    throw new InvalidEnumArgumentException("mode is invalid");
                    break;
            }

            var uri = BuildUri(
                new KeyValuePair<string, object>[]
                                   {
                                       new KeyValuePair<string, object>(_IrisMode, irisModeValue),
                                       new KeyValuePair<string, object>(propertyName, level)
                                   }
                                   );

            SetCameraProperty(uri);

        }

        public void SetAgc(bool AgcEnable, bool digitalGainEnable)
        {
            int agcModeValue = AgcEnable ? 1: 0;
            int digitalGainValue = digitalGainEnable ? 1: 0;

            string uri = BuildUri(new KeyValuePair<string, object>[]
                                            {
                                                new KeyValuePair<string, object> (_AgcMode, agcModeValue),
                                                new KeyValuePair<string, object> (_DigitalGain, digitalGainValue), 
                                            });
            SetCameraProperty(uri);
            

        }

        public static void SearchCamersAsync()
        {
            Thread t = new Thread(DoSearch);
            t.IsBackground = true;

            UdpClient udp = new UdpClient(PCPort);

            t.Start(udp);
            Thread.Sleep(3000);
            t.Interrupt();
            t.Join();
        }

        private static void DoSearch(object state)
        {
            UdpClient udp = (UdpClient)state;

            Header h = new Header(Command.IpQueryStart, MAC.BroadCast);
            byte[] data = BitConverter.GetBytes((ushort)3);

            udp.EnableBroadcast = true;

            IPEndPoint brdcstIp = new IPEndPoint(System.Net.IPAddress.Broadcast, CameraPort);
            SendCommand(udp, brdcstIp, h, data);

            IPEndPoint srcHost = new IPEndPoint(System.Net.IPAddress.Any, 0);

            byte[] reply = null;

            do
            {
                try
                {
                    reply = udp.Receive(ref srcHost);
                }
                catch (System.Threading.ThreadInterruptedException)
                {
                    return;
                }

                if (reply.Length == 44)
                {
                    Command c = (Command)BitConverter.ToUInt16(reply, 0);
                    if (c == Command.IpQueryReply) HandleIpQueryReplyPacket(reply, udp, brdcstIp);
                }

            } while (reply.Length > 0);
        }


        private static void HandleIpQueryReplyPacket(byte[] buffer, UdpClient udp, IPEndPoint dst)
        {
            byte[] ipBytes = new byte[4];
            Array.Copy(buffer, 32, ipBytes, 0, ipBytes.Length);
            IPAddress ip = new IPAddress(ipBytes);
            string ipString = ip.ToString();

            if (!ipFound.Contains(ipString)) ipFound.Add(ipString);

            Header h = new Header(Command.IpQueryReplyConfirm, MAC.BroadCast);
            Header ipSearchReplyHdr = Header.Parse(buffer, 0);
            SendCommand(udp, dst, h, ipSearchReplyHdr.Mac.GetBytes());

        }


        private static void SendCommand(UdpClient udp, IPEndPoint dest, Header hdr, byte[] data)
        {
            byte[] hdrBytes = hdr.GetBytes();

            byte[] buffer = new byte[hdrBytes.Length + data.Length];
            hdrBytes.CopyTo(buffer, 0);
            data.CopyTo(buffer, hdrBytes.Length);

            udp.Send(buffer, buffer.Length, dest);
        }



        public SanyoNetCamera(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        private void EnsureConnected()
        {
            if (!connected)
            {
                throw new InvalidOperationException("not connected to camera");
            }
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string IPAddress { get; set; }

        bool connected = false;

        public void Connect()
        {
            string uri = string.Format("http://{0}", IPAddress);

            var request = this.CreateHttpWebRequest(uri, new CookieContainer());

            HttpWebResponse reply = (HttpWebResponse)request.GetResponse();
            cookies = request.CookieContainer;

            connected = true;

        }


        enum Command : ushort
        {
            IpQueryStart = 0x4031, IpQueryReply = 0x0031, IpQueryReplyConfirm = 0x8031,
        }

        class UdpState
        {
            public UdpClient u;
            public bool canceled;
        }

        class Header
        {
            byte[] buffer = new byte[32];


            public Header() { }

            public Header(Command cmd, MAC destMAC)
            {
                this.Cmd = cmd;
                this.Mac = destMAC;

                this.PackNo = 1;
                this.TotalNumOfPackets = 1;
                this.SeqNo = 1;
            }

            public Command Cmd
            {
                get { return (Command)this.GetUshort(0); }
                set { this.SetShort((ushort)value, 0); }
            }

            public MAC Mac
            {
                get { return new MAC(buffer, 2); }
                set { value.GetBytes().CopyTo(buffer, 2); }
            }

            public ushort SeqNo
            {
                get { return this.GetUshort(8); }
                set { this.SetShort(value, 8); }
            }

            public ushort Ver
            {
                get { return this.GetUshort(10); }
                set { this.SetShort(value, 10); }
            }

            public ushort PackNo
            {
                get { return this.GetUshort(12); }
                set { this.SetShort(value, 12); }
            }

            public ushort TotalNumOfPackets
            {
                get { return this.GetUshort(14); }
                set { this.SetShort(value, 14); }
            }

            public byte[] GetBytes()
            {
                return (byte[])buffer.Clone();
            }

            public static Header Parse(byte[] buffer, int startIndex)
            {
                Header h = new Header();
                buffer.CopyTo(h.buffer, startIndex);
                return h;
            }

            private void SetShort(ushort value, int index)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                bytes.CopyTo(buffer, index);
            }

            private ushort GetUshort(int index)
            {
                return BitConverter.ToUInt16(buffer, index);
            }

        }

        static int CameraPort = 10001;
        static int PCPort = 10000;
        static IList<string> ipFound = new List<string>();
        static object locker = new object();
        static volatile bool cancel;

        CookieContainer cookies;

        #region ICamera Members

        public System.Drawing.Image CaptureImage()
        {
            throw new NotImplementedException();
        }

        public byte[] CaptureImageBytes()
        {
            EnsureConnected();

            string uri = string.Format("http://{0}/liveimg.cgi", IPAddress);
            var req = CreateHttpWebRequest(uri, this.cookies);

            var reply = (HttpWebResponse)req.GetResponse();

            long len = reply.ContentLength;
            byte[] buff = new byte[len];

            Stream stream = reply.GetResponseStream();

            long count = 0;

            do
            {
                count += stream.Read(buff, (int)count, (int)(len - count));
            } while (count < len);

            return buff;
        }


        public bool Record
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Start()
        {


            throw new NotImplementedException();
        }

        #endregion


        private HttpWebRequest CreateHttpWebRequest(string uri, CookieContainer cookies)
        {
            return CreateHttpWebRequest(uri, cookies, "GET", false);
        }


        private HttpWebRequest CreateHttpWebRequest(string uri, CookieContainer cookies, string method, bool adminPrivilegeRequired)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(uri);
            req.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;

            if (adminPrivilegeRequired)
            {
                req.Credentials = new NetworkCredential("admin", "admin");
            }
            else
            {
                req.Credentials = new NetworkCredential(UserName, Password);
            }

            req.ProtocolVersion = new Version(1, 1);
            req.CookieContainer = cookies;
            req.KeepAlive = true;
            req.PreAuthenticate = true;
            req.Method = method;
            req.Timeout = 10000;
            req.Proxy = System.Net.GlobalProxySelection.GetEmptyWebProxy();

            return req;
        }

        private HttpWebResponse GetHttpRequest(string uri, CookieContainer cookies, bool adminPrivilegeRequired)
        {
            return this.SendHttpRequest(uri, cookies, "GET", adminPrivilegeRequired);
        }

        private HttpWebResponse PostHttpRequest(string uri, CookieContainer cookies, bool adminPrivilegeRequired)
        {
            return this.SendHttpRequest(uri, cookies, "POST", adminPrivilegeRequired);
        }

        private HttpWebResponse SendHttpRequest(string uri, CookieContainer cookies, string method, bool adminPrivilegeRequired)
        {
            var request = this.CreateHttpWebRequest(uri, cookies, method, adminPrivilegeRequired);

            return (HttpWebResponse)request.GetResponse();
        }


        private void SetCameraProperty(string uri)
        {
            EnsureConnected();

            PostHttpRequest(uri, this.cookies, true);
        }


        #region IDisposable Members

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
