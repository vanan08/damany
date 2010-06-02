using System;

namespace SearchCamera
{
    internal static class PacketFactory
    {
        public static Packet<short> CreateDistributionStart(PacketType type)
        {
            var p = new DistributionStartDataPacket();
            p.Header.Command = 0x4031;
            p.Data = 1;
            return p;
        }



    }
}