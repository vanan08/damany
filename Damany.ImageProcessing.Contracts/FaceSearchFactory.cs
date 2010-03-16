using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;

namespace Damany.Imaging.Processors
{

    public static class FaceSearchFactory
    {
        public static FaceSearchController CreateNewController(IFrameStream source)
        {
            PortraitFinder finder = new PortraitFinder();

            MotionDetector motionDetector = new MotionDetector();
            motionDetector.MotionFrameCaptured += finder.HandleMotionFrame;

            Damany.Util.PersistentWorker retriever = CreateDriver(source, motionDetector);

            var controller = new FaceSearchController()
            {
                Worker = retriever,
                PortraitFinder = finder,
                MotionDetector = motionDetector
            };

            return controller;

        }


        private static Damany.Util.PersistentWorker CreateDriver(IFrameStream source, MotionDetector motionDetector)
        {
            var retriever = new Damany.Util.PersistentWorker();
            retriever.OnWorkItemIsDone += item =>
            {
                Console.Write("\r");
                Frame f = item as Frame;
                Console.Write(f.ToString());
            };

            retriever.DoWork = delegate
            {
                var frame = source.RetrieveFrame();
                retriever.ReportWorkItem(frame);
                motionDetector.DetectMotion(frame);
            };

            retriever.OnExceptionRetry = delegate { source.Connect(); };
            return retriever;
        }
    }
}
