using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace Damany.ImageProcessing.Contracts
{
    public class MotionFrame : Frame
    {
        public IList<CvRect> MotionRectangles { get; set; }
    }
}
