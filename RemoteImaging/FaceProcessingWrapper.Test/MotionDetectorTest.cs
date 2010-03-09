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

            var f = new Damany.Imaging.Contracts.Frame(bytes);
            Debug.WriteLine("guid: " + f.Guid.ToString());

            Guid guidFram1 = f.Guid;

            MotionDetectionResult result = new MotionDetectionResult();

            detector.PreProcessFrame(f, result);
            Assert.IsTrue(Guid.Empty.Equals(result.FrameGuid));

            bytes = System.IO.File.OpenRead(@"D:\ImageOutput\02\2010\01\27\BigPic\201001271121\02_100127112121953.jpg");
            f = new Damany.Imaging.Contracts.Frame(bytes);
            detector.PreProcessFrame(f, result);

            Assert.IsTrue(result.FrameGuid.Equals(guidFram1));
        }
    }
}
