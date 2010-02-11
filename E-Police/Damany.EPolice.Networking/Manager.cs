using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.EPolice.Networking.Parsers;

namespace Damany.EPolice.Networking
{

    public class Manager
    {

        public Manager(ISplitter splitter)
        {
            this.splitter = splitter;
            this.parsers = new List<IParser>();

        }

        public void Start()
        {
            var client = new System.Net.Sockets.TcpClient();
            client.Connect(System.Net.IPAddress.Parse(Configuration.RemoteIp), Configuration.RemotePort);

            System.Threading.Thread thread = new System.Threading.Thread(this.StartInternal);
            thread.IsBackground = true;
            thread.Start(client.GetStream());
        }

        public IList<IParser> Parsers
        {
            get
            {
                return parsers;
            }
        }

        private void StartInternal(object userData)
        {
            System.IO.Stream stream = (System.IO.Stream) userData;
            while (true)
            {
                var packetBuffer = this.splitter.ReadNext(stream);
                HandlePacket(packetBuffer);
            }
        }

        private void HandlePacket(Damany.EPolice.Networking.Packets.BinaryPacket packetBuffer)
        {
            foreach (var parser in this.Parsers)
            {
                if (parser.CanParse(packetBuffer))
                {
                    parser.Parse(packetBuffer);
                    parser.NotifyListener();
                    return;
                }
            }
        }
     
        private IList<IParser> parsers;
        private ISplitter splitter;

    }
}
