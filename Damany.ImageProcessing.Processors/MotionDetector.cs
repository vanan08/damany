﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Processors
{
    using Contracts;


    public class MotionDetector
    {
        public MotionDetector()
        {
            this.manager = new FrameManager();
            this.detector = new FaceProcessingWrapper.MotionDetector();
        }

        public void DetectMotion(Frame frame)
        {
            this.manager.AddNewFrame(frame);

            FaceProcessingWrapper.MotionDetectionResult oldFrameMotionResult;
            bool groupCaptured = ProcessNewFrame(frame, out oldFrameMotionResult);

            ProcessOldFrame(oldFrameMotionResult);

            if (groupCaptured)
            {
                NotifyListener();
            }
        }


        private bool ProcessNewFrame(Frame frame, out FaceProcessingWrapper.MotionDetectionResult detectionResult)
        {
            detectionResult = new FaceProcessingWrapper.MotionDetectionResult();

            return this.detector.PreProcessFrame(frame, detectionResult);
        }


        private static bool IsStaticFrame(OpenCvSharp.CvRect rect)
        {
            return rect.Width == 0 || rect.Height == 0;
        }


        private void ProcessOldFrame(FaceProcessingWrapper.MotionDetectionResult result)
        {
            if (IsStaticFrame(result.MotionRect))
            {
                this.manager.DisposeFrame(result.FrameGuid);
            }
            else
            {
                this.manager.MoveToMotionFrames(result);
            }
        }

        private void NotifyListener()
        {
            var frames = this.manager.RetrieveMotionFrames();

            if (this.MotionFrameCaptured != null)
            {
                this.MotionFrameCaptured(frames);
            }

            
        }

        public event Action<IList<Contracts.Frame>> MotionFrameCaptured;

        FaceProcessingWrapper.MotionDetector detector;

        FrameManager manager;
    }
}
