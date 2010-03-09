﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace Damany.Imaging.Contracts
{
    public class Frame : CapturedObject, IComparable<Frame>, IEquatable<Frame>, IDisposable
    {
        public Frame(System.IO.Stream stream)
        {
            this.lazyIpl = new LazyIplImage(stream);
            this.InitializeFields();
        }

        public Frame(OpenCvSharp.IplImage ipl)
        {
            this.iplImage = ipl;
            this.InitializeFields();
        }


        #region IComparable<Frame> Members

        public int CompareTo(Frame other)
        {
            CheckForDisposed();

            return this.Guid.CompareTo(other.Guid);
        }

        #endregion

        #region IEquatable<Frame> Members

        public bool Equals(Frame other)
        {
            CheckForDisposed();
            return this.Guid.Equals(other.Guid);
        }

        #endregion


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
                if (lazyIpl != null)
                {
                    lazyIpl.Dispose();
                }

                if (this.iplImage != null)
                {
                    this.iplImage.Dispose();
                }

            }

            this.lazyIpl = null;
            this.iplImage = null;
            disposed = true;
        }

        #endregion


        public Frame Clone()
        {
            CheckForDisposed();
            var clone = new Frame();

            clone.lazyIpl = this.lazyIpl == null ? null : this.lazyIpl.Clone();
            clone.CapturedAt = this.CapturedAt;
            clone.CapturedFrom = this.CapturedFrom;
            clone.Guid = this.Guid;
            clone.iplImage = this.iplImage == null ? null : this.iplImage.Clone();

            return clone;
        }


        public OpenCvSharp.IplImage Ipl
        {
            get
            {
                CheckForDisposed();
                if (this.iplImage == null)
                {
                    this.iplImage = this.lazyIpl.Ipl;
                }

                return this.iplImage;
            }
        }

        private Frame() {}

        private void InitializeFields()
        {
            CheckForDisposed();
            this.CapturedAt = DateTime.Now;
            this.Guid = System.Guid.NewGuid();
        }


        private void CheckForDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException("Frame");
            }
        }


        public List<CvRect> MotionRectangles { get; private set; }
        public List<PortraitBounds> Portraits { get; private set; }

        LazyIplImage lazyIpl;
        OpenCvSharp.IplImage iplImage;

        bool disposed;

    }
}
