using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using OpenCvSharp;

namespace ImageProcess
{
    public class Frame
    {
        public byte cameraID;
        /// IplImage*
        public OpenCvSharp.IplImage image;

        public CvRect searchRect;

        /// int
        public long timeStamp;
    }
}
