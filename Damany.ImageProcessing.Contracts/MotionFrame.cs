using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace Damany.ImageProcessing.Contracts
{
    public class MotionFrame : Frame
    {
        public MotionFrame(IplImage image)
            : base(image)
        {
            
        }

        public MotionFrame(System.IO.Stream stream)
            : base(stream)
        {
        }

        public MotionFrame Clone()
        {
            var clone = new MotionFrame(base.Clone().Ipl);

            var rectsClone =
                this.MotionRectangles == null ?
                null : new List<CvRect>(this.MotionRectangles);

            clone.MotionRectangles = rectsClone;

            return clone;
        }

        protected override void Dispose(bool IsDisposing)
        {
            if (disposed) return;

            base.Dispose(IsDisposing);
            disposed = true;
        }

        private void InitializeFields()
        {
            this.MotionRectangles = new List<OpenCvSharp.CvRect>();
        }

        public List<CvRect> MotionRectangles { get; private set; }

        bool disposed;
    }
}
