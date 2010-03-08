using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace Damany.ImageProcessing.Contracts
{
    public class Portrait : IDisposable
    {
        public Portrait(MotionFrame from)
        {
            this.From = from;
        }

        public IplImage FaceImage
        {
            get
            {
                CheckForDisposed();

                if (faceImage == null)
                {

                    faceImage = this.From.Ipl.GetSub(this.BoundsInParent.FaceBounds);
                }

                return this.faceImage;

            }
        }

        public IplImage PortraitImage
        {
            get
            {
                CheckForDisposed();

                if (portraitImage == null)
                {

                    portraitImage = this.From.Ipl.GetSub(this.BoundsInParent.FaceBounds);
                }

                return this.portraitImage;


            }
        }



        #region IDisposable Members

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool IsDisposing)
        {
            if (disposed) return;
            
            if (IsDisposing)
            {
                if (this.From != null)
                {
                    this.From.Dispose();
                }

                if (this.faceImage != null)
                {
                    this.faceImage.Dispose();
                }

                if (this.portraitImage != null)
                {
                    this.portraitImage.Dispose();
                }
            }

            this.From = null;
            this.faceImage = null;
            this.portraitImage = null;

            this.disposed = true;

        }

        #endregion


        private void CheckForDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException("Portrait");
            }
        }


        public PortraitBounds BoundsInParent { get; set; }
        public MotionFrame From { get; private set; }
        IplImage faceImage;
        IplImage portraitImage;

        bool disposed;
    }
}
