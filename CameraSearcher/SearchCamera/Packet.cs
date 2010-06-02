using System.IO;

namespace SearchCamera
{
    internal class Packet<T>
    {

        public Packet()
        {
            Header = new Header();
        }

        public byte[] AssemblePacket(PacketAssembler assembler)
        {
            assembler.AppendHeader(this.Header);
            AppendData(assembler);

            return assembler.GetBuffer();
        }

        public virtual void AppendData(PacketAssembler assembler) { }

        public virtual void Send(PacketTransmitter transmitter, byte[] buffer)
        {
            transmitter.BroadCast(buffer);
        }


        public Header Header { get; set; }
        public T Data { get; set; }
    }
}