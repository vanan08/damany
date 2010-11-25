using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace Damany.Imaging.Common
{
    public class MotionDetectionResult
    {
        public CvRect MotionRect { get; set; }
        public Guid FrameGuid { get; set; }
    }
}
