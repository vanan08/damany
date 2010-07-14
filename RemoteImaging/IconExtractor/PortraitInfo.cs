using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcess
{
    public class PortraitInfo
    {
        public OpenCvSharp.IplImage Face;
        public OpenCvSharp.CvRect FacesRect;
        public OpenCvSharp.CvRect FacesRectForCompare;
    }
}
