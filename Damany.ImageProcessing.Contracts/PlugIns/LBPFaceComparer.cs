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
    [Export(typeof(IRepositoryFaceComparer))]
    public class LbpFaceComparer : IRepositoryFaceComparer, IConfigurable
    {
        public void Load(IList<Common.PersonOfInterest> persons)
        {
            this.persons = persons;

            var ipls = from p in persons
                       select p.Ipl.CvtToGray();

            this.lbp.Load(ipls.ToArray());
        }

        public void SetSensitivity(float value)
        {
            this.sensitivity = value;
        }

        public RepositoryCompareResult[] CompareTo(IplImage image)
        {
            var results = this.lbp.CompareTo(image.CvtToGray());

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
    }
}