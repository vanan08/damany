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
    public class LbpFaceComparer : IFaceComparer, IConfigurable
    {
        public void SetSensitivity(float value)
        {
            this.sensitivity = value;
        }

        public FaceCompareResult Compare(IplImage a, IplImage b)
        {
            var roiA = a.ROI;
            var roiB = b.ROI;

            var grayA = a.CvtToGray();
            var grayB = b.CvtToGray();
#if DEBUG
            var cloneA = grayA.Clone();
            cloneA.DrawRect(roiA, CvColor.Red, 2);
            var bmpA = cloneA.ToBitmap();
            

            var cloneB = grayB.Clone();
            cloneB.DrawRect(roiB, CvColor.Red, 2);
            var bmpB = cloneB.ToBitmap();
#endif

            float score = 0.0f;

            grayA.ResetROI();
            grayB.ResetROI();
            bool similar = StaticFunctions.LBPCompareFace(grayA, roiA, grayB, roiB, ref score, sensitivity);
            

            grayA.Dispose();
            grayB.Dispose();

            var result = new FaceCompareResult() { IsSimilar = similar, SimilarScore = score };
            return result;
        }

        #region IFaceComparer Members


        public string Name
        {
            get { return "Lbp Face Comparer"; }
        }

        public string Description
        {
            get { return "Lbp Face Compare Algorithm By Damany"; }
        }

 
        public Guid UUID
        {
            get { return  uuid; }
        }

        #endregion

        private static System.Guid uuid = new Guid("{5E7780E6-C093-4694-B3A4-C60B4659BA57}");

        #region IConfigurable Members

        public object GetConfig()
        {
            return new LbpConfiguration();
        }

        public void SetConfig(object config)
        {
            
        }

        #endregion

        private float sensitivity = 35;
    }
}