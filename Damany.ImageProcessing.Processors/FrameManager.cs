using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.ImageProcessing.Processors
{
    using Contracts;

    class FrameManager
    {
        public FrameManager()
        {
            this.motionFrames = new List<MotionFrame>();
            this.frameHistory = new Dictionary<Guid, Frame>();
        }

        public void AddNewFrame(Frame f)
        {
            this.frameHistory[f.Guid] = f;
        }

        public IList<MotionFrame> RetrieveMotionFrames()
        {
            var toReturn = this.motionFrames;
            this.motionFrames = null;
            return toReturn;
        }

        public void MoveToMotionFrames( FaceProcessingWrapper.MotionDetectionResult frameResult )
        {
            var f = RetrieveFrame(frameResult.FrameGuid);
            if (f != null)
            {
                if (this.motionFrames == null)
                {
                    this.motionFrames = new List<MotionFrame>();
                }

                Damany.ImageProcessing.Contracts.MotionFrame motionFrame = CreateMotionFrame(frameResult.MotionRect, f);
                motionFrames.Add(motionFrame);
            }
        }

        public void DisposeFrame(System.Guid guidOfFrameToDispose)
        {
            var f = this.RetrieveFrame(guidOfFrameToDispose);
            if (f != null)
            {
                f.Dispose();
            }
        }

        private static Damany.ImageProcessing.Contracts.MotionFrame CreateMotionFrame(OpenCvSharp.CvRect rect, Damany.ImageProcessing.Contracts.Frame f)
        {
            var motionRects = new List<OpenCvSharp.CvRect>();
            motionRects.Add(rect);

            var motionFrame = new MotionFrame(f, motionRects);
            return motionFrame;
        }

        private Frame RetrieveFrame(Guid id)
        {
            if (frameHistory.ContainsKey(id))
            {
                var returnFrame = frameHistory[id];
                frameHistory.Remove(id);
                return returnFrame;
            }

            return null;
        }


        private void DisposeMotionFrames()
        {
            if (this.motionFrames == null) return;

            foreach (var item in this.motionFrames)
            {
                item.Dispose();
            }
        }

        Dictionary<Guid, Frame> frameHistory;
        IList<MotionFrame> motionFrames;

    }
}
