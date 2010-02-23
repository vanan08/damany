using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

namespace FaceProcessingWrapper.Test
{
    [TestFixture]
    public class MotionDetectorTest
    {
        [Test]
        public void Test()
        {
            FaceProcessingWrapper.MotionDetector detector = new FaceProcessingWrapper.MotionDetector();

            ImageProcess.Frame f = new ImageProcess.Frame();
            f.image = OpenCvSharp.IplImage.FromFile(@"D:\ImageOutput\02\2010\01\27\BigPic\201001271121\02_100127112121328.jpg");

            ImageProcess.Frame toDispose=null;

            detector.PreProcessFrame(f, out toDispose);
            Assert.IsNotNull(toDispose);

            f.image = OpenCvSharp.IplImage.FromFile(@"D:\ImageOutput\02\2010\01\27\BigPic\201001271121\02_100127112121953.jpg");
            detector.PreProcessFrame(f, out toDispose);
            Assert.IsNotNull(toDispose.image);


        }
    }
}
