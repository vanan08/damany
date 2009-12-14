using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ImageProcess
{
    public class Target
    {

        /// Frame
        public Frame BaseFrame;

        public OpenCvSharp.IplImage[] Faces;

        ///CvRect*
        public OpenCvSharp.CvRect[] FacesRects;

        //人脸比对新加的CvRect*
        public OpenCvSharp.CvRect[] FacesRectsForCompare;
    }
}
