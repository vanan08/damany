using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.EPolice.Networking.Packets;

namespace Damany.EPolice.Networking
{
    using Common.Util;

    public class PacketSplitter : Parsers.ParserBase, ISplitter
    {
        #region ISplitter Members

        public BinaryPacket ReadNext(System.IO.Stream stream)
        {
            using (var reader = new MiscUtil.IO.EndianBinaryReader(Configuration.EndianBitConverter, stream, Configuration.Encoding))
            {
                var type = reader.ReadInt32();

                var bufferLen = reader.ReadInt32();
                var buffer = reader.ReadBytes(bufferLen);

                var packet = new Packets.BinaryPacket();
                packet.Tag = (uint)type;
                packet.PayLoadBuffer = buffer;

                return packet;
            }

        }

        #endregion
    }
}
