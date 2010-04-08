using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;
using Damany.Util.Extensions;

namespace Damany.Imaging.Common
{
    public class Portrait : CapturedObject, IDisposable
    {
        public Portrait(string path)
        {
            this.path = path;
        }

        public Portrait(IplImage portraitImage)
        {
            this.portraitImage = portraitImage;
        }

        public Portrait Clone()
        {
            var clone = new Portrait(this.portraitImage.Clone());

            this.CopyPropertiesTo(clone);

            return clone;
        }


        public IplImage GetIpl()
        {
            CheckForDisposed();

            if (this.portraitImage == null)
            {
                this.portraitImage = OpenCvSharp.IplImage.FromFile(this.path);
            }

            return this.portraitImage;
        }

        public override string ToString()
        {
            var str = string.Format("{0}x{1} : {2}", this.portraitImage.Width, this.portraitImage.Height, this.CapturedAt);
            return str;
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
                if (this.portraitImage != null)
                {
                    this.portraitImage.Dispose();
                }
            }

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


        public OpenCvSharp.CvRect FaceBounds { get; set; }
        public Guid FrameId { get; set; }

        IplImage portraitImage;
        string path;

        bool disposed;
    }
}
