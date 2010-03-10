using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Processors
{
    using Damany.Imaging.Contracts;

    public class FramePumper
    {
        public FramePumper(IFrameStream source)
        {
            if (source == null)
                throw new ArgumentNullException("source", "source is null.");

            this.frameSource = source;
        }

        public void Pump()
        {
            if (this.MotionDetector == null)
            {
                throw new InvalidOperationException("MotionDetector is null");
            }

            var frame = this.frameSource.RetrieveFrame();
            this.MotionDetector.DetectMotion(frame);
        }

        public MotionDetector MotionDetector { get; set; }

        IFrameStream frameSource;
    }
}
