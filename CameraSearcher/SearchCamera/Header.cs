namespace SearchCamera
{
    public class Header
    {
        public Header()
        {
            Mac = new byte[6];
            SequenceNumber = 0x01;
            PacketNumber = 0x01;
            TotalPacket = 0x01;
            ReserverByte = new byte[16];
        }

        public short Command;
        public byte[] Mac;
        public short SequenceNumber;
        public short Version;
        public short PacketNumber;
        public short TotalPacket;
        public byte[] ReserverByte;
    }
}