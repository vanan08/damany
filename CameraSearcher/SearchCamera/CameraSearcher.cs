using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace SearchCamera
{
    public class CameraSearcher
    {
        private UdpClient _socket;

        public EventHandler<CameraFoundArgs> CameraFound;


        public CameraSearcher()
        {
            LocalPort = 10000;
            CameraPort = 10001;
        }

        public void Search()
        {
            _socket = new UdpClient(LocalPort);
            System.Threading.ThreadPool.QueueUserWorkItem(this.SendQuery);
            System.Threading.ThreadPool.QueueUserWorkItem(this.RcvReply);
        }

        public int LocalPort { get; set; }
        public int CameraPort { get; set; }

        private void SendQuery(object state)
        {
            var transmitter = new PacketTransmitter(_socket);
            transmitter.BroadcastPort = CameraPort;

            var packet = PacketFactory.CreateDistributionStart(PacketType.DistributionStart);
            var packetAssembler = new PacketAssembler();

            var buffer = packet.AssemblePacket(packetAssembler);

            while (true)
            {
                packet.Send(transmitter, buffer);
                System.Threading.Thread.Sleep(1000);
            }

        }

        private void RcvReply(object state)
        {
            while (true)
            {
                var remoteEndpoint = new IPEndPoint(IPAddress.Any, 0);
                _socket.Receive(ref remoteEndpoint);

                if (CameraFound != null)
                {
                    var args = new CameraFoundArgs();
                    args.CameraIp = remoteEndpoint.Address.ToString();
                    CameraFound(this, args);
                }
            }
        }
    }
}
