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
    public partial class SanyoNetCamera : System.ComponentModel.Component, ICamera
    {
        public SanyoNetCamera()
        {
            InitializeComponent();
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

        public string UserName { get; set; }
        public string Password { get; set; }
        public string IPAddress { get; set; }

        public void Connect()
        {
            string uri = string.Format("http://{0}", IPAddress);

            HttpWebRequest reqAuthorize = (HttpWebRequest)HttpWebRequest.Create(uri);
            reqAuthorize.Timeout = 1000 * 5;
            reqAuthorize.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
            reqAuthorize.ProtocolVersion = new Version(1, 1);
            reqAuthorize.Credentials = new NetworkCredential(UserName, Password);
            reqAuthorize.CookieContainer = new CookieContainer();
            reqAuthorize.KeepAlive = true;
            reqAuthorize.Proxy = System.Net.GlobalProxySelection.GetEmptyWebProxy();

            HttpWebResponse reply = (HttpWebResponse)reqAuthorize.GetResponse();
            cookies = reqAuthorize.CookieContainer;

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
            string uri = string.Format("http://{0}/liveimg.cgi", IPAddress);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(uri);
            req.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
            req.Credentials = new NetworkCredential(UserName, Password);
            req.ProtocolVersion = new Version(1, 1);
            req.CookieContainer = cookies;
            req.KeepAlive = true;
            req.Proxy = System.Net.GlobalProxySelection.GetEmptyWebProxy();


            HttpWebResponse reply = (HttpWebResponse)req.GetResponse();

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
    }
}
