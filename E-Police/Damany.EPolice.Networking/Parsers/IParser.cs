using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.EPolice.Networking.Parsers
{
    public interface IParser
    {
        bool CanParse(Packets.BinaryPacket binaryPacket);

        void Parse(Packets.BinaryPacket binaryPacket);

        void NotifyListener();
    }
}
