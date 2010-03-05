using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.ImageProcessing.Contracts
{
    public class Frame
    {
        public byte[] BitmapData { get; set; }
        public DateTime CapturedAt { get; set; }
        public IFrameStream CapturedFrom { get; set; }
    }
}
