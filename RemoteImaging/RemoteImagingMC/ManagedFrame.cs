using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging
{
    public class ManagedFrame
    {
        public byte CameraID;
        public OpenCvSharp.IplImage Ipl;
        public DateTime TimeStamp;
        public OpenCvSharp.CvRect MotionRect;
    }
}
