using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using Damany.Imaging.Extensions;
using OpenCvSharp;

namespace FaceProcessingWrapper.Test
{
    [TestFixture]
    public class FaceSearcherTest
    {
        [Test]
        public void Test()
        {
            var p = TestDataProvider.Data.GetPortrait();

            foreach (var file in System.IO.Directory.GetFiles(@"M:\测试图片\公司人脸", "*.jpg"))
            {
//                var img = OpenCvSharp.IplImage.FromFile(file);
//
//                var rects1 = img.LocateFaces();
//                if (rects1.Length == 0)
//                {
//                    continue;
//                }
//
//                var rects2 = img.LocateFaces(new CvRect(0, 0, img.Width, img.Height));
//                if (rects2.Length == 0)
//                {
//                    continue;
//                }
//
//                System.Diagnostics.Debug.WriteLine( rects1[0].ToString() + "other: " + rects2[0].ToString() );
//
//                Assert.IsTrue(rects2[0].Equals(rects1[0]));

                
            }

            
        }
    }
}
