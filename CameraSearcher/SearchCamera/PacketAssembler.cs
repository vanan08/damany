using System;
using System.IO;
using System.Net;

namespace SearchCamera
{
    public class PacketAssembler
    {
        private readonly MemoryStream _memStream;
        private readonly BinaryWriter _streamWriter;

        public PacketAssembler()
        {
            _memStream = new MemoryStream();
            _streamWriter = new BinaryWriter(_memStream);

        }

        public void AppendHeader(Header header)
        {
            _streamWriter.Write(IPAddress.HostToNetworkOrder(header.Command));
            _streamWriter.Write(header.Mac);
            _streamWriter.Write(IPAddress.HostToNetworkOrder(header.SequenceNumber));
            _streamWriter.Write(IPAddress.HostToNetworkOrder(header.Version));
            _streamWriter.Write(IPAddress.HostToNetworkOrder(header.PacketNumber));
            _streamWriter.Write(IPAddress.HostToNetworkOrder(header.TotalPacket));
            _streamWriter.Write(header.ReserverByte);
        }

        public void AppendData(int data)
        {
            _streamWriter.Write(data);
        }

        public void AppendData(short data)
        {
            _streamWriter.Write(IPAddress.HostToNetworkOrder(data));
        }

        public byte[] GetBuffer()
        {
            _streamWriter.Flush();
            return _memStream.GetBuffer();
        }

    }
}