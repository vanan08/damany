using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Common
{
    public class LazyIplImage : IDisposable
    {

        public LazyIplImage(System.IO.Stream stream)
        {
            this.stream = stream;
        }


        public OpenCvSharp.IplImage Ipl
        {
            get
            {
                if (this.ipl == null)
                {
                    using (var bmp = new System.Drawing.Bitmap(this.stream))
                    {
                        this.ipl = OpenCvSharp.IplImage.FromBitmap(bmp);
                    }

                }

                return this.ipl;
            }
        }


        #region IDisposable Members

        public void Dispose()
        {
            if (this.ipl != null)
            {
                this.ipl.Dispose();
                this.ipl = null;
            }

            if (this.stream != null)
            {
                this.stream.Dispose();
                this.stream = null;
            }
        }

        #endregion


        public LazyIplImage Clone()
        {
            var clone = new LazyIplImage();

            if (this.stream != null)
            {
                var stream = new System.IO.MemoryStream();
                MiscUtil.IO.StreamUtil.Copy(this.stream, stream);
                clone.stream = stream;
            }

            clone.ipl = this.ipl == null ? null : this.ipl.Clone();

            return clone;
        }

        private LazyIplImage() { }

        System.IO.Stream stream;
        OpenCvSharp.IplImage ipl;

    }
}
