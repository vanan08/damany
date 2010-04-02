using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Algorithms;
using FaceProcessingWrapper;
using OpenCvSharp;

namespace Damany.Imaging.PlugIns
{
    public class LBPFaceComparer : IFaceComparer
    {

        public FaceCompareResult Compare(OpenCvSharp.IplImage a, OpenCvSharp.IplImage b)
        {
            var grayA = new IplImage(a.ROI.Size, BitDepth.U8, 1);
            a.CvtColor(grayA,ColorConversion.BgrToGray);

            var grayB = new IplImage(b.ROI.Size, BitDepth.U8, 1);
            b.CvtColor(grayB, ColorConversion.BgrToGray);

#if DEBUG
            var bmpA = grayA.ToBitmap();
            var bmpB = grayB.ToBitmap();
#endif

            float score = 0.0f;
            bool similar = StaticFunctions.LBPCompareFace(grayA, grayA.ROI, grayB, grayB.ROI, ref score);

            grayA.Dispose();
            grayB.Dispose();

            var result = new FaceCompareResult() { IsSimilar = similar, SimilarScore = score };
            return result;
        }
    }
}