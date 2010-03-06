using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace Damany.ImageProcessing.Contracts
{
    public class Portrait : IDisposable
    {
        public MotionFrame ContainedIn { get; set; }
        public BitmapIplUnion FaceImage { get; set; }
        public CvRect FaceRect { get; set; }
        public CvRect RectInMotionFrame { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            if (this.ContainedIn != null)
            {
                this.ContainedIn.Dispose();
                this.ContainedIn = null;
            }

            if (this.FaceImage != null)
            {
                this.FaceImage.Dispose();
                this.FaceImage = null;
            }
        }

        #endregion
    }
}
