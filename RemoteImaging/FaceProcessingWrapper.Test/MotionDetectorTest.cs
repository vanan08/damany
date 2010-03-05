using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using System.Diagnostics;

namespace FaceProcessingWrapper.Test
{
    [TestFixture]
    public class MotionDetectorTest
    {
        [Test]
        public void Test()
        {
            FaceProcessingWrapper.MotionDetector detector = new FaceProcessingWrapper.MotionDetector();

            var bytes = System.IO.File.OpenRead(@"D:\ImageOutput\02\2010\01\27\BigPic\201001271121\02_100127112121328.jpg");

            var f = new Damany.ImageProcessing.Contracts.Frame(bytes);
            Debug.WriteLine("guid: " + f.Guid.ToString());

            Guid guidFram1 = f.Guid;

            Guid guidOut = new Guid();
            OpenCvSharp.CvRect rectOut = new OpenCvSharp.CvRect();

            detector.PreProcessFrame(f, out guidOut, out rectOut);

            Assert.IsTrue(rectOut.Width == 0 && rectOut.Height == 0);

            bytes = System.IO.File.OpenRead(@"D:\ImageOutput\02\2010\01\27\BigPic\201001271121\02_100127112121953.jpg");
            f = new Damany.ImageProcessing.Contracts.Frame(bytes);
            detector.PreProcessFrame(f, out guidOut, out rectOut);

            Assert.IsTrue(guidOut.Equals(guidFram1));
            Assert.IsTrue(rectOut.Width != 0 && rectOut.Height != 0);



        }
    }
}
