using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.ImageProcessing.Contracts
{
    public class Frame : GuidObject, IComparable<Frame>, IEquatable<Frame>
    {
        public Frame(System.IO.Stream stream)
        {
            this.image = new BitmapIplUnion(stream);
        }

        public System.IO.Stream BitmapData { get; set; }
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
    }
}
