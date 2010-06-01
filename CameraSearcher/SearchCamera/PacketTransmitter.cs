using System.IO;
using System.Net;
using System.Net.Sockets;

namespace SearchCamera
{
    public class PacketTransmitter
    {
        private readonly UdpClient _socket;

        public PacketTransmitter(UdpClient socket)
        {
            _socket = socket;
            BroadcastPort = 10001;
        }

        public void BroadCast(byte[] buffer)
        {
            var remoteEndpoint = new IPEndPoint(IPAddress.Broadcast, BroadcastPort);

            _socket.Send(buffer, buffer.Length, remoteEndpoint);

        }


        public int BroadcastPort { get; set; }

    }
}