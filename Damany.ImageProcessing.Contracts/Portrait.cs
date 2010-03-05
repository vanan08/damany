using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace Damany.ImageProcessing.Contracts
{
    public class Portrait
    {
        public MotionFrame ContainedIn { get; set; }
        public IplImage FaceImage { get; set; }
        public CvRect FaceRect { get; set; }
        public CvRect RectInMotionFrame { get; set; }
    }
}
