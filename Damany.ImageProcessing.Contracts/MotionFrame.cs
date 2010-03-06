using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace Damany.ImageProcessing.Contracts
{
    public class MotionFrame : IDisposable
    {
        public MotionFrame(Frame frame, IList<CvRect> rects)
        {
            this.Frame = frame;
            this.MotionRectangles = rects;
        }

        public IList<CvRect> MotionRectangles { get; set; }
        public Frame Frame { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            if (this.Frame != null)
            {
                this.Frame.Dispose();
                this.Frame = null;
            }
        }

        #endregion

        public MotionFrame Clone()
        {
            var frameClone = this.Frame == null ? null : this.Frame.Clone();
            var rectsClone =
                this.MotionRectangles == null ?
                null : new List<CvRect>(this.MotionRectangles);

            return new MotionFrame(frameClone, rectsClone);
        }
    }
}
