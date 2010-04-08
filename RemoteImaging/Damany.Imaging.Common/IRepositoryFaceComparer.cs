using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Common
{
    public interface IRepositoryFaceComparer
    {
        void Load(IList<PersonOfInterest> persons);
        void SetSensitivity(float sensitivity);
        RepositoryCompareResult[] CompareTo(OpenCvSharp.IplImage image);
    }
}
