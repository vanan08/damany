using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;

namespace Damany.Imaging.Processors
{

    public class MotionDetector
    {
        private readonly IMotionDetector _detector;
        OpenCvSharp.CvSize lastImageSize;
        readonly FrameManager _manager = new FrameManager();


        public event Action<IList<Frame>> MotionFrameCaptured;

        public MotionDetector(IMotionDetector detector)
        {
            _detector = detector;
        }


        public bool ProcessFrame(Frame frame)
        {
            try
            {
                frame.GetImage();
            }
            catch (System.ArgumentException ex)
            {
                return false;
            }

            this._manager.AddNewFrame(frame);

            var oldFrameMotionResult = new MotionDetectionResult();
            bool groupCaptured =
                ProcessNewFrame(frame, ref oldFrameMotionResult);

            ProcessOldFrame(oldFrameMotionResult);

            return groupCaptured;

        }

        public List<Frame> GetMotionFrames()
        {
            return _manager.RetrieveMotionFrames();
        }


        private bool ProcessNewFrame(Frame frame, ref MotionDetectionResult detectionResult)
        {
            var result = _detector.Detect(frame, ref detectionResult);

            return result;
        }


        private static bool IsStaticFrame(OpenCvSharp.CvRect rect)
        {
            return rect.Width == 0 || rect.Height == 0;
        }


        private void ProcessOldFrame(MotionDetectionResult result)
        {
            if (IsStaticFrame(result.MotionRect))
            {
                this._manager.DisposeFrame(result.FrameGuid);
            }
            else
            {
                this._manager.MoveToMotionFrames(result);
            }
        }

        private void NotifyListener()
        {
            var frames = this._manager.RetrieveMotionFrames();
            if (frames.Count == 0) return;

            if (this.MotionFrameCaptured != null)
            {
                this.MotionFrameCaptured(frames);
            }
        }

        private bool ImageResolutionChanged(Frame currentFrame)
        {
            return currentFrame.GetImage().Size != lastImageSize;
        }

    }
}
