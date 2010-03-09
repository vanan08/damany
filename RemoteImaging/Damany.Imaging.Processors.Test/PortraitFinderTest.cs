using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using Damany.Cameras;
using Damany.Imaging.Handlers;
using Damany.Imaging.Processors;

namespace Damany.Imaging.Processors.Test
{
    [TestFixture]
    public class PortraitFinderTest
    {
        [Test]
        [Timeout(1*60*60)]
        public void Test()
        {
            var source = new DirectoryFilesCamera(@"z:\", "*.jpg");
            source.Initialize();

            var motionFrameLogger = new MotionFrameLogger();

            var portraitWriter = new PortraitsLogger(@".\Portrait");
            portraitWriter.Initialize();
            
            var asyncPortraitWriter = new AsyncPortraitLogger(@".\AsyncPortrait");
            asyncPortraitWriter.Initialize();

            var portraitFinder = new PortraitFinder();
            portraitFinder.AddListener(asyncPortraitWriter);
            portraitFinder.AddListener(portraitWriter);

            var motionDetector = new MotionDetector(portraitFinder);


            for (int i = 0; i < 1100;++i )
            {
                var frame = source.RetrieveFrame();
                motionDetector.DetectMotion(frame);
            }

            
            
        }
    }

    public class MotionFrameLogger : Imaging.Contracts.IMotionFrameHandler
    {

        #region IMotionFrameHandler Members

        public void HandleMotionFrame(IList<Damany.Imaging.Contracts.Frame> motionFrames)
        {
            motionFrames.ToList().ForEach(frame =>
            {
                frame.Ipl.SaveImage(frame.Guid.ToString() + ".jpg");
                frame.Dispose();
            });
            
        }

        #endregion
    }
}
