using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.EPolice.Networking.Packets
{
    using Common;

    public class LicensePlatePacket
    {
        public LicensePlate LicensePlate { get; set; }
        public DateTime CaptureTime { get; set; }
        public Location CaptureLocation { get; set; }
        public IList<byte[]> EvidenceImageData { get; set; }
    }
}
