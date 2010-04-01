using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Algorithms;
using FaceProcessingWrapper;

namespace Damany.Imaging.PlugIns
{
    public class LBPFaceComparer : IFaceComparer
    {

        public FaceCompareResult Compare(OpenCvSharp.IplImage a, OpenCvSharp.IplImage b)
        {
            float score = 0.0f;
            bool similar = StaticFunctions.LBPCompareFace(a, a.ROI, b, b.ROI, ref score);

            var result = new FaceCompareResult() { IsSimilar = similar, SimilarScore = score };
            return result;
        }
    }
}