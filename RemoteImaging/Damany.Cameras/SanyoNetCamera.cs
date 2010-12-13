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
using Damany.Imaging.Common;

namespace Damany.Cameras
{
    using NameObjectCollection = Dictionary<string, object>;

    public partial class SanyoNetCamera : IIpCamera, IDisposable
    {
        const string _ShutterMode = "shutter_sw";
        const string _ShutterShortSpeedLevel = "short_speed";
        const string _ShutterLongSpeedLevel = "long_speed";

        const string _IrisMode = "iris_sw";
        const string _IrisManualLevel = "manu_level";
        const string _IrisAutoLevel = "auto_level";

        const string _AgcMode = "agc_sw";
        const string _DigitalGain = "digital_gain";


        NameObjectCollection properties;

        public SanyoNetCamera()
        {

        }

        public IrisMode IrisMode
        {
            get
            {
                EnsureUpdateCalled();

                return (IrisMode)properties[_IrisMode];
            }
        }

        public int ManualIrisLevel
        {
            get
            {
                EnsureUpdateCalled();

                return (int)properties[_IrisManualLevel];
            }
        }

        public bool AgcEnabled
        {
            get
            {
                EnsureUpdateCalled();

                return ((int)(properties[_AgcMode])) == 0 ? false : true;
            }
        }

        public bool DigitalGainEnabled
        {
            get
            {
                EnsureUpdateCalled();

                return ((int)(properties[_DigitalGain])) == 0 ? false : true;
            }
        }

        public ShutterMode ShutterMode
        {
            get
            {
                EnsureUpdateCalled();

                return (ShutterMode)(properties[_ShutterMode]);
            }
        }

        public int ShortShutterLevel
        {
            get
            {
                EnsureUpdateCalled();

                return (int)properties[_ShutterShortSpeedLevel];
            }
        }

        private static KeyValuePair<string, object> ParseSemicommaSplittedLine(string line)
        {
            string[] subStrings = line.Split(new char[] { ':' });
            if (subStrings.Length != 2)
            {
                throw new ArgumentException("line is not splitted by semi comma(:)");
            }

            string valueString = subStrings[1].TrimStart('(').TrimEnd(')');
            return new KeyValuePair<string, object>(subStrings[0], int.Parse(valueString));
        }

        private static bool ShouldSkip(string line)
        {
            return line == "end"
                    || line == "200 OK"
                    || line == "[camera_quality_cgi]"
                    || string.IsNullOrEmpty(line);
        }

        private static NameObjectCollection ParseCameraProperties(System.IO.Stream replyStream)
        {
            var nameValues = new NameObjectCollection();
            using (StreamReader rdr = new StreamReader(replyStream))
            {
                while (true)
                {
                    var line = rdr.ReadLine();

                    if (line == null)//end of stream
                    {
                        break;
                    }

                    if (ShouldSkip(line)) continue;

                    try
                    {
                        var keyValue = ParseSemicommaSplittedLine(line);
                        if (!nameValues.ContainsKey(keyValue.Key))
                        {
                            nameValues.Add(keyValue.Key, keyValue.Value);
                        }
                    }
                    catch (System.ArgumentException) { }

                }

                return nameValues;

            }
        }

        public void UpdateProperty()
        {
            EnsureConnected();

            var nameValues = new NameObjectCollection();
            nameValues.Add("status", 1);

            var uri = this.BuildUri(nameValues);
            var reply = this.GetHttpRequest(uri, this.cookies, true);

            try
            {
                var properties = ParseCameraProperties(reply.GetResponseStream());
                this.properties = properties;
            }
            finally
            {
                reply.Close();
            }


        }

        private string BuildUri(NameObjectCollection nameValues)
        {
            string parameters = string.Join("&", nameValues.Select(n => n.Key + "=" + n.Value.ToString()).ToArray());

            var uri = String.Format("{0}//cgi-bin//camera_quality.cgi?{1}", this.Uri, parameters);

            return uri;

        }

        public void SetShutter(ShutterMode mode, int speedLevel)
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

            var nameValues = new NameObjectCollection();
            nameValues.Add(_ShutterMode, modeValue);
            nameValues.Add(speedPropertyName, speedLevel);

            string uri = BuildUri(nameValues);

            SetCameraProperty(uri);
        }

        public void SetIris(IrisMode mode, int level)
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


            var nameValues = new NameObjectCollection();
            nameValues.Add(_IrisMode, irisModeValue);
            nameValues.Add(propertyName, level);

            var uri = BuildUri(nameValues);

            SetCameraProperty(uri);

        }

        public void SetAGCMode(bool AgcEnable, bool digitalGainEnable)
        {
            int agcModeValue = AgcEnable ? 1 : 0;
            int digitalGainValue = digitalGainEnable ? 1 : 0;


            var nameValues = new NameObjectCollection();
            nameValues.Add(_AgcMode, agcModeValue);
            nameValues.Add(_DigitalGain, digitalGainValue);

            string uri = BuildUri(nameValues);

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

        private void EnsureConnected()
        {
            if (!connected)
            {
                throw new InvalidOperationException("not connected to camera");
            }
        }

        private void EnsureUpdateCalled()
        {
            if (this.properties == null)
            {
                throw new InvalidOperationException("call UpdateProperty first");
            }
        }


        public string UserName { get; set; }
        public string PassWord { get; set; }
        public Uri Uri { get; set; }
        public string Description { get { return "Sanyo Ip Camera"; } }

        bool connected = false;

        public void Connect()
        {

            var request = this.CreateHttpWebRequest(this.Uri.ToString(), new CookieContainer());

            HttpWebResponse reply = null;
            try
            {
                reply = (HttpWebResponse)request.GetResponse();
                cookies = request.CookieContainer;
                connected = true;

            }
            finally
            {
                if (reply != null)
                {
                    reply.Close();
                    reply.GetResponseStream().Close();
                }
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

            var uri = new Uri(this.Uri, "liveimg.cgi").ToString();
            var req = CreateHttpWebRequest(uri, this.cookies);

            var reply = (HttpWebResponse)req.GetResponse();
            Stream stream = reply.GetResponseStream();
            try
            {
                long len = reply.ContentLength;
                byte[] buff = new byte[len];
                long count = 0;
                while (true)
                {
                    int bytesRead = stream.Read(buff, (int)count, (int)(len - count));
                    if (bytesRead <= 0)
                    {
                        break;
                    }

                    count += bytesRead;
                    if (count == len)
                    {
                        break;
                    }
                }

                return buff;

            }
            catch (System.IO.IOException ex)
            {
                throw new System.Net.WebException("capture frame error", ex);
            }
            finally
            {
                if (reply != null)
                {
                    reply.Close();
                }

                if (stream != null)
                {
                    stream.Close();
                }

            }

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

        public Uri Location { get; set; }
        public int ID { get; set; }

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
                req.Credentials = new NetworkCredential(UserName, PassWord);
            }

            req.ProtocolVersion = new Version(1, 1);
            req.CookieContainer = cookies;
            req.KeepAlive = true;
            req.PreAuthenticate = true;
            req.Method = method;
            req.Timeout = 1 * 1000;
            System.Net.WebRequest.DefaultWebProxy = null;

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

            var reply = PostHttpRequest(uri, this.cookies, true);
            reply.Close();
        }


        #region IDisposable Members

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion


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


        enum Command : ushort
        {
            IpQueryStart = 0x4031, IpQueryReply = 0x0031, IpQueryReplyConfirm = 0x8031,
        }

        class UdpState
        {
            public UdpClient u;
            public bool canceled;
        }


        #region IFrameStream Members

        public void Initialize()
        {

        }

        public void Close()
        {

        }

        public Frame RetrieveFrame()
        {
            try
            {
                var stream = new MemoryStream(this.CaptureImageBytes());
                var frame = new Frame(stream);
                frame.CapturedFrom = this;
                return frame;
            }
            catch (System.Exception ex)
            {
                this.connected = false;
                throw;
            }

        }

        public int Id { get; set; }

        #endregion
    }
}
