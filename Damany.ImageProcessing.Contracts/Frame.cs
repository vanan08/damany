using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.ImageProcessing.Contracts
{
    public class Frame : GuidObject, IComparable<Frame>, IEquatable<Frame>, IDisposable
    {
        public Frame(System.IO.Stream stream)
        {
            this.image = new BitmapIplUnion(stream);
        }

       
        public DateTime CapturedAt { get; set; }
        public IFrameStream CapturedFrom { get; set; }

        public System.Drawing.Bitmap Bitmap
        {
            get
            {
                return this.image.Bitmap;
            }
        }

        public OpenCvSharp.IplImage Ipl
        {
            get
            {
                return this.image.Ipl;
            }
        }

        #region IComparable<Frame> Members

        public int CompareTo(Frame other)
        {
            return this.Guid.CompareTo(other.Guid);
        }

        #endregion

        #region IEquatable<Frame> Members

        public bool Equals(Frame other)
        {
            return this.Guid.Equals(other.Guid);
        }

        #endregion

        BitmapIplUnion image;

        #region IDisposable Members

        public void Dispose()
        {
            if (image != null)
            {
                image.Dispose();
                this.image = null;
            }
        }

        #endregion


        public Frame Clone()
        {
            var clone = new Frame();

            clone.image = this.image == null ? null : this.image.Clone();
            clone.CapturedAt = this.CapturedAt;
            clone.CapturedFrom = this.CapturedFrom;

            return clone;
        }


        private Frame() {}
    }
}
