using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Algorithms;

namespace Damany.Imaging.PlugIns
{
    public interface IFaceComparer
    {
        FaceCompareResult Compare(OpenCvSharp.IplImage a, OpenCvSharp.IplImage b);
    }
}