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

        public System.Drawing.Bitmap Bitmap
        {
            get
            {
                if (this.bitmap == null)
                {
                    this.bitmap = (System.Drawing.Bitmap) System.Drawing.Bitmap.FromStream(this.stream);
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
            }

            if (this.bitmap != null)
            {
                this.bitmap.Dispose();
            }

            if (this.stream != null)
            {
                this.stream.Dispose();
            }
        }

        #endregion
    }
}
