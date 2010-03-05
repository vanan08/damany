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
            ImageProcess.Frame f = new ImageProcess.Frame();
            
            this.detector.PreProcessFrame(


        }


        FaceProcessingWrapper.MotionDetector detector;
        IMotionFrameHandler handler;

    }
}
