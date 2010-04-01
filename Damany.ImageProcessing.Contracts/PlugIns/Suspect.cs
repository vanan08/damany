using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace Damany.Imaging.PlugIns
{
    public class Suspect
    {
        public Suspect(IplImage image)
        {
            this.Ipl = image;
        }


        public IplImage Ipl { get; private set; }


    }
}
