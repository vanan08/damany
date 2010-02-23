using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.EPolice.Networking.Packets
{
    public class BinaryPacket
    {
        public BinaryPacket()
        {
            this.Header = new Dictionary<string, object>();
        }

        public IDictionary<string, object> Header { get; set; }
        public byte[] PayLoadBuffer { get; set; }
        public object Tag { get; set; }
    }
}
