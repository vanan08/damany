using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace RemoteImaging
{
    internal class Face
    {
        public IplImage Img { get; set; }
        public CvRect EnlargedFace { get; set; }
        public CvRect OriginalFace { get; set; }
    }
}
