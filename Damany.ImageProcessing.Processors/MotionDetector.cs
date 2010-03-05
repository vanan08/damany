using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.ImageProcessing.Processors
{
    using Contracts;


    public class MotionDetector
    {
        public void DetectMotion(Frame frame)
        {
            System.Guid guidOfFrameToDispose = new System.Guid();
            OpenCvSharp.CvRect rect = new OpenCvSharp.CvRect();
            
            this.detector.PreProcessFrame(frame, out guidOfFrameToDispose, out rect);


        }


        FaceProcessingWrapper.MotionDetector detector;
        IMotionFrameHandler handler;

    }
}
