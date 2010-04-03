using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Algorithms;
using FaceProcessingWrapper;
using OpenCvSharp;
using Damany.Util;

namespace Damany.Imaging.PlugIns
{
    public class LBPFaceComparer : IFaceComparer
    {

        public FaceCompareResult Compare(OpenCvSharp.IplImage a, OpenCvSharp.IplImage b)
        {
            var roiA = a.ROI;
            var roiB = b.ROI;

            var grayA = a.CvtToGray();
            var grayB = b.CvtToGray();
#if DEBUG
            var bmpA = grayA.ToBitmap();
            var bmpB = grayB.ToBitmap();
#endif

            float score = 0.0f;

            grayA.ResetROI();
            grayB.ResetROI();
            bool similar = StaticFunctions.LBPCompareFace(grayA, roiA, grayB, roiB, ref score);
            

            grayA.Dispose();
            grayB.Dispose();

            var result = new FaceCompareResult() { IsSimilar = similar, SimilarScore = score };
            return result;
        }
    }
}