using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.ImageProcessing.Contracts
{
    public class BitmapIplUnion : IDisposable
    {
        public BitmapIplUnion(System.IO.Stream bytesStream)
        {
            this.stream = bytesStream;
        }

        public BitmapIplUnion(System.Drawing.Bitmap bmp)
        {
            this.bitmap = bmp;
        }

        public BitmapIplUnion(OpenCvSharp.IplImage ipl)
        {
            this.ipl = ipl;
        }

        public System.Drawing.Bitmap Bitmap
        {
            get
            {
                if (this.bitmap == null)
                {
                    if (this.stream != null)
                    {
                        this.bitmap = (System.Drawing.Bitmap)System.Drawing.Bitmap.FromStream(this.stream);
                    }
                    else if (this.ipl != null)
                    {
                        this.bitmap = OpenCvSharp.BitmapConverter.ToBitmap(this.ipl);
                    }
                    else
                        throw new InvalidOperationException("Object is not initialized correctly");
                }

                return this.bitmap;
            }
        }

        public OpenCvSharp.IplImage Ipl 
        {
            get
            {
                if (this.ipl == null)
                {
                    this.ipl = OpenCvSharp.IplImage.FromBitmap(this.Bitmap);
                }

                return this.ipl;
            }
        }


        System.IO.Stream stream;
        System.Drawing.Bitmap bitmap;
        OpenCvSharp.IplImage ipl;

        #region IDisposable Members

        public void Dispose()
        {
            if (this.ipl != null)
            {
                this.ipl.Dispose();
                this.ipl = null;
            }

            if (this.bitmap != null)
            {
                this.bitmap.Dispose();
                this.bitmap = null;
            }

            if (this.stream != null)
            {
                this.stream.Dispose();
                this.stream = null;
            }
        }

        #endregion


        public BitmapIplUnion Clone()
        {
            var clone = new BitmapIplUnion();

            if (this.stream != null)
            {
                var stream = new System.IO.MemoryStream();
                MiscUtil.IO.StreamUtil.Copy(this.stream, stream);
                clone.stream = stream;
            }

            clone.ipl = this.ipl == null ? null : this.ipl.Clone();
            clone.bitmap = this.bitmap == null ? null : (System.Drawing.Bitmap) this.bitmap.Clone();

            return clone;
        }

        private BitmapIplUnion() {}
    }
}
