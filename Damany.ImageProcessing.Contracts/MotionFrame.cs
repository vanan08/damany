using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace Damany.ImageProcessing.Contracts
{
    public class MotionFrame
    {
        public MotionFrame(Frame frame, IList<CvRect> rects)
        {
            this.Frame = frame;
            this.MotionRectangles = rects;
        }

        public IList<CvRect> MotionRectangles { get; set; }
        public Frame Frame { get; set; }
    }
}
