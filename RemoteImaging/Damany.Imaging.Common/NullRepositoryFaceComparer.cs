using System;
using System.Collections.Generic;
using OpenCvSharp;

namespace Damany.Imaging.Common
{
    public class NullRepositoryFaceComparer : IRepositoryFaceComparer
    {
        public void Load(IList<PersonOfInterest> persons)
        {
           
        }

        public void SetSensitivity(float sensitivity)
        {
            
        }

        public RepositoryCompareResult[] CompareTo(IplImage image)
        {
            return new RepositoryCompareResult[0];
        }
    }
}