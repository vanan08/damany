using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Contracts
{
    public class PortraitBounds
    {

        public OpenCvSharp.CvRect Bounds { get; set; }
        public OpenCvSharp.CvRect FaceBoundsInPortrait { get; set; }
    }
}
