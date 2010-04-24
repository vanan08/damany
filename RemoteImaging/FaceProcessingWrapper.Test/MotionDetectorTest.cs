using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using System.Diagnostics;

namespace FaceProcessingWrapper.Test
{
    [TestFixture]
    public class MotionDetectorTest
    {
        public void Test()
        {
            IMotionDetector motiondetector = new FaceProcessingWrapper.MotionDetector();

            foreach (var file in System.IO.Directory.GetFiles(@"M:\测试图片\Lb", "*.jpg"))
            {
                var ipl = OpenCvSharp.IplImage.FromFile(file);
                var frame = new Frame(ipl);

                MotionDetectionResult result = new MotionDetectionResult();
                var groupCaptured = motiondetector.Detect(frame, ref result);

                System.Diagnostics.Debug.WriteLine(groupCaptured.ToString() + " " + result.MotionRect.Width);

            }
        }

        [Test]
        public void MotionDetectorHlTest()
        {
            IMotionDetector algorithm = new FaceProcessingWrapper.MotionDetector();

            var dir = new Damany.Cameras.DirectoryFilesCamera(@"M:\测试图片\Lb", "*.jpg");
            dir.Initialize();
            var detector = new Damany.Imaging.Processors.MotionDetector(algorithm);

            while (true)
            {
                detector.Execute(null);
            } 
            
        }
    }
}
