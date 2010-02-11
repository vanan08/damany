using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.EPolice.Networking
{

    public class Manager
    {
        static Manager()
        {
            InitParsers();

            fiber = new Retlang.Fibers.ThreadFiber();
            fiber.Start();
        }


        private static void InitParsers()
        {
            parsers = new Parsers.IParser[]
		            {
		                new Parsers.LicensePlateParser(),
		            };
        }


        public void SubscribeLicensePlate(Action<Packets.LicensePlatePacket> handler)
        {
            if (licensePlateChannel == null)
            {
                licensePlateChannel = new Retlang.Channels.Channel<Packets.LicensePlatePacket>();
            }

            licensePlateChannel.Subscribe(fiber, handler);

        }

        public void Start()
        {
            var client = new System.Net.Sockets.TcpClient();
            client.Connect(System.Net.IPAddress.Parse(Configuration.RemoteIp), Configuration.RemotePort);

            var splitter = new PacketSplitter(client.GetStream());
            splitter.PacketCaptured += new EventHandler<MiscUtil.EventArgs<Damany.EPolice.Networking.Packets.Raw>>(splitter_PacketCaptured);
            splitter.Start();
        }

        void splitter_PacketCaptured(object sender, MiscUtil.EventArgs<Damany.EPolice.Networking.Packets.Raw> e)
        {
            foreach (var parser in parsers)
            {
                var buffer = e.Value.Buffer;
                if (parser.CanParse(e.Value.Type))
                {
                    var packet = parser.Parse(buffer, 0, buffer.Length);

                    if (packet is Packets.LicensePlatePacket)
                    {
                        licensePlateChannel.Publish( (Packets.LicensePlatePacket) packet);
                    }

                    break;
                }
            }
            
        }




        private static Retlang.Fibers.IFiber fiber;
        private static Retlang.Channels.Channel<Packets.LicensePlatePacket> licensePlateChannel;
        private static Parsers.IParser[] parsers;
        
    }
}
