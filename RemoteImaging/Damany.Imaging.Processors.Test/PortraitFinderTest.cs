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
        [Timeout(1 * 60 * 60)]
        public void Test()
        {
            var source = new DirectoryFilesCamera(@"z:\", "*.jpg");
            source.Initialize();

            var motionFrameLogger = new FrameWritter();

            var portraitWriter = new PortraitsLogger(@".\Portrait");
            portraitWriter.Initialize();

            var asyncPortraitWriter = new AsyncPortraitLogger(@".\AsyncPortrait1");
            asyncPortraitWriter.Stopped += (o, e) => System.Diagnostics.Debug.WriteLine(e.Value.Message);
            asyncPortraitWriter.Initialize();

            var asyncWriter1 = new AsyncPortraitLogger(@".\asyncport2");
            asyncWriter1.Initialize();

            var portraitFinder = new PortraitFinder();

            asyncPortraitWriter.Start();


            bool running = true;
            for (int i = 0; i < 25 && running; ++i)
            {
                var frame = source.RetrieveFrame();

            }

        }
    }


}
