using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.ImageProcessing.Processors
{
    using Contracts;


    public class MotionDetector
    {
        public MotionDetector(IMotionFrameHandler handler)
        {
            this.handler = handler;
            this.manager = new FrameManager();
        }

        public void DetectMotion(Frame frame)
        {
            this.manager.AddNewFrame(frame);

            System.Guid guidOfFrameToBeHandled = new System.Guid();
            OpenCvSharp.CvRect rect = new OpenCvSharp.CvRect();
            
            bool groupCaptured = this.detector.PreProcessFrame(frame, out guidOfFrameToBeHandled, out rect);

            if (IsStaticFrame(rect))
            {
                this.manager.DisposeFrame(guidOfFrameToBeHandled);
            }
            else
            {
                this.manager.MoveToMotionFrames(guidOfFrameToBeHandled, rect);
            }


            if (groupCaptured)
            {
                var frames = this.manager.MotionFrames;
                this.handler.HandleMotionFrame(frames);
                frames.Dispose();
            }
        }


        private static bool IsStaticFrame(OpenCvSharp.CvRect rect)
        {
            return rect.Width == 0 || rect.Height == 0;
        }


        
        FaceProcessingWrapper.MotionDetector detector;
        IMotionFrameHandler handler;

        FrameManager manager;
    }
}
