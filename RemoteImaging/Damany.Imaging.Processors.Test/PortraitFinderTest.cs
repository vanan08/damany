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
        public void Test()
        {
            var source = new DirectoryFilesCamera(@"z:\", "*.jpg");
            source.Initialize();

            var portraitHandler = new PortraitsHandler();
            var portraitFinder = new PortraitFinder(portraitHandler);
            var motionDetector = new MotionDetector(portraitFinder);

            

            for (int i = 0; i < 20;++i )
            {
                var frame = source.RetrieveFrame();
                motionDetector.DetectMotion(frame);
            }

            
            
        }
    }
}
