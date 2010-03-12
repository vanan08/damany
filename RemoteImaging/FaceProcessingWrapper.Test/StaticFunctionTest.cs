using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

using FaceProcessingWrapper;

namespace FaceProcessingWrapper.Test
{
    [TestFixture]
    public class StaticFunctionTest
    {
        [Test]
        public void Test()
        {
            var ipl = OpenCvSharp.IplImage.FromFile(@"D:\20090505\02_090613112921-0003.jpg");
            float similar = 0;


            bool IsSimilar = StaticFunctions.CompareFace(ipl, new OpenCvSharp.CvRect(0, 0, 100, 100),
                ipl, new OpenCvSharp.CvRect(0, 0, 100, 100), ref similar, false);
            
        }
    }
}
