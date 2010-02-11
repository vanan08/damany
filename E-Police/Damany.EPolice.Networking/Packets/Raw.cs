using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.EPolice.Networking.Packets
{
    struct Raw
    {
        public uint Type { get; set; }
        public byte[] Buffer { get; set; }
    }
}
