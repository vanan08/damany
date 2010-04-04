using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;
using FaceProcessingWrapper;
using OpenCvSharp;
using Damany.Util;
using System.ComponentModel.Composition;

namespace Damany.Imaging.PlugIns
{
    [Export(typeof(IFaceComparer))]
    public class LbpFaceComparer : IFaceComparer
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

        #region IFaceComparer Members


        public string Name
        {
            get { return "Lbp FaceComparer"; }
        }

        public string Description
        {
            get { return "Lbp Face Compare Algorithm By Damany"; }
        }

        public bool CanConfig
        {
            get { return false; }
        }

        public System.Windows.Forms.Control ConfigControl
        {
            get { throw new NotImplementedException(); }
        }

        public Guid UUID
        {
            get { return  uuid; }
        }

        #endregion

        private static System.Guid uuid = new Guid("{5E7780E6-C093-4694-B3A4-C60B4659BA57}");
    }
}