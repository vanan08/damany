using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;
using Damany.Imaging.Extensions;
using FaceProcessingWrapper;
using FaceSearchWrapper;
using OpenCvSharp;
using Damany.Util;
using System.ComponentModel.Composition;

namespace Damany.Imaging.PlugIns
{
    [Export(typeof(IRepositoryFaceComparer))]
    public class LbpFaceComparer : IRepositoryFaceComparer, IConfigurable
    {
        public void Load(IList<Common.PersonOfInterest> persons)
        {
            if (persons == null) throw new ArgumentNullException("persons");
            foreach (var personOfInterest in persons)
            {
                if ( personOfInterest.Ipl.BoundsRect().Equals(personOfInterest.Ipl.ROI) )
                {
                    throw new System.ArgumentException("Roi is same as bouding rect");
                }

                if (personOfInterest.Ipl.NChannels == 1)
                {
                    throw new System.ArgumentException("gray image is not supported");
                }
            }

            this.persons = persons;

            foreach (var p in persons)
            {
                p.Ipl.CheckWithBmp();
            } 


            var ipls = from p in persons
                       select p.Ipl.CvtToGray().GetSub(p.Ipl.ROI);



            this.lbp.Load(ipls.ToArray());
        }

        public void SetSensitivity(float value)
        {
            this.sensitivity = value;
        }

        public RepositoryCompareResult[] CompareTo(IplImage image)
        {
            if (image == null) throw new ArgumentNullException("image");
            if (image.NChannels == 1)
            {
                throw new ArgumentException("gray image is not supported");
            }

            var roi = image.ROI;
            image.ResetROI();
            var gray = image.CvtToGray();

            //relocate face to be more precise.
            var faceRects = image.LocateFaces(searcher);
            if (faceRects.Length > 0)
            {
                gray.ROI = faceRects[0];
            }

            image.ROI = gray.ROI;
            image.CheckWithBmp();
            image.SetROI(roi);

            var results = this.lbp.CompareTo(gray);

            var returnResult = new RepositoryCompareResult[this.persons.Count];

            for (int i = 0; i < returnResult.Length; i++)
            {
                var r = new RepositoryCompareResult();
                r.PersonInfo = persons[i];
                r.Similarity = results[i];

                returnResult[i] = r;
            }

            return returnResult;
        }

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
        private FaceProcessingWrapper.LbpWrapper lbp = new LbpWrapper();
        private IList<Common.PersonOfInterest> persons;

        private FaceSearchWrapper.FaceSearch searcher = new FaceSearch();
    }
}