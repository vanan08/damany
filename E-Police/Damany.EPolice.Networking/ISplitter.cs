using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.EPolice.Networking.Packets;

namespace Damany.EPolice.Networking
{
    public interface ISplitter
    {
        BinaryPacket ReadNext(System.IO.Stream stream);
    }
}
