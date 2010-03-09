﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace Damany.Imaging.Contracts
{
    public class Portrait : CapturedObject, IDisposable
    {
        public Portrait(IplImage portraitImage)
        {
            this.portraitImage = portraitImage;

            this.Guid = System.Guid.NewGuid();
            this.Bounds = new PortraitBounds();
        }


        public IplImage PortraitImage
        {
            get
            {
                CheckForDisposed();

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


        public PortraitBounds Bounds { get; set; }
        public Guid FrameId { get; set; }

        IplImage portraitImage;

        bool disposed;
    }
}
