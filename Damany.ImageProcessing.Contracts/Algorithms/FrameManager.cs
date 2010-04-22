using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Processors
{
    using Common;

    class FrameManager
    {
        public FrameManager()
        {
            this.motionFrames = new List<Frame>();
            this.frameHistory = new Dictionary<Guid, Frame>();
        }

        public void AddNewFrame(Frame f)
        {
            this.frameHistory[f.Guid] = f;
        }

        public List<Frame> RetrieveMotionFrames()
        {
            var toReturn = this.motionFrames;
            this.motionFrames = null;
            return toReturn;
        }

        public void MoveToMotionFrames( MotionDetectionResult frameResult )
        {
            var f = RetrieveFrame(frameResult.FrameGuid);
            if (f != null)
            {
                if (this.motionFrames == null)
                {
                    this.motionFrames = new List<Frame>();
                }

                f.MotionRectangles.Add(frameResult.MotionRect);
                motionFrames.Add(f);
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
        List<Frame> motionFrames;

    }
}
