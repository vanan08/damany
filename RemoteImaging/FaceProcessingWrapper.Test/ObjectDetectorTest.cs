using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using System.Linq;

namespace FaceProcessingWrapper.Test
{
    [TestFixture]
    public class ObjectDetectorTest
    {
        [Test]
        public void Test()
        {
            var detector = new ObjectDetector();
            

            Action<string> action = file =>
                                        {
                                            if (System.IO.Path.GetExtension(file) != ".jpg")
                                            {
                                                return;
                                            }

                                            var img = OpenCvSharp.IplImage.FromFile(file);
                                            var rects = detector.ProcessFrame(img);
                                            Debug.WriteLine(rects.Length);
                                            Array.ForEach(rects, r => Debug.Write(r));
                                            Debug.WriteLine(string.Empty);
                                        };

            TestDataProvider.Data.VisitDirectory(@"M:\测试图片\Lb", action);
            
        }
    }
}
