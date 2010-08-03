﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FaceProcessingWrapper;

namespace Damany.Imaging.Processors
{


    public class MotionDetector
    {

        public MotionDetector()
        {
            this.manager = new FrameManager();
            this.detector = new FaceProcessingWrapper.MotionDetector();
            this.DetectMethod = this.detector.PreProcessFrame;
        }


        public Func<Contracts.Frame, FaceProcessingWrapper.MotionDetectionResult, bool> DetectMethod { get; set; }
       

        public void DetectMotion(Frame frame)
        {
            try
            {
                frame.GetImage();
            }
            catch (System.ArgumentException ex)
            {
                return;
            }
             
            this.manager.AddNewFrame(frame);

            FaceProcessingWrapper.MotionDetectionResult oldFrameMotionResult;
            bool groupCaptured = 
                ProcessNewFrame(frame, out oldFrameMotionResult);

            ProcessOldFrame(oldFrameMotionResult);

            if (groupCaptured)
            {
                NotifyListener();
            }

        }


        private bool ProcessNewFrame(Frame frame, out FaceProcessingWrapper.MotionDetectionResult detectionResult)
        {
            detectionResult = new FaceProcessingWrapper.MotionDetectionResult();

            return this.DetectMethod(frame, detectionResult);
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

        public event Action<IList<Contracts.Frame>> MotionFrameCaptured;

        FaceProcessingWrapper.MotionDetector detector;
        OpenCvSharp.CvSize lastImageSize;

        FrameManager manager;
    }
}
