using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;

namespace Damany.Imaging.Processors
{

    public class MotionDetector : IOperation<Frame>
    {
        private readonly IMotionDetector _detector;
        private readonly IFrameStream _frameStream;
        OpenCvSharp.CvSize lastImageSize;
        readonly FrameManager _manager = new FrameManager();


        public event Action<IList<Frame>> MotionFrameCaptured;

        public MotionDetector(IMotionDetector detector, IFrameStream frameStream)
        {
            _detector = detector;
            _frameStream = frameStream;
        }

        #region IOperation<Frame> Members

        public IEnumerable<Frame> Execute(IEnumerable<Frame> input)
        {
            var frame = _frameStream.RetrieveFrame();

            System.Diagnostics.Debug.WriteLine("get frame: " + frame.ToString());

            return DetectMotion(frame);
        }

        #endregion

        private IEnumerable<Frame> DetectMotion(Frame frame)
        {
            try
            {
                frame.GetImage();
            }
            catch (System.ArgumentException ex)
            {
                yield break;
            }

            this._manager.AddNewFrame(frame);

            var oldFrameMotionResult = new MotionDetectionResult();
            bool groupCaptured =
                ProcessNewFrame(frame, out oldFrameMotionResult);

            ProcessOldFrame(oldFrameMotionResult);

            if (groupCaptured)
            {
                var frames = _manager.RetrieveMotionFrames();
                foreach (var f in frames)
                {
                    yield return f;
                }
            }

        }


        private bool ProcessNewFrame(Frame frame, out MotionDetectionResult detectionResult)
        {
            return _detector.Detect(frame, out detectionResult);
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
