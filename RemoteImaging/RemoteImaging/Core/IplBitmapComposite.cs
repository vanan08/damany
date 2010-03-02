using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OpenCvSharp;

namespace RemoteImaging.Core
{
    class IplBitmapComposite : System.IDisposable
    {
        private IplImage ipl;
        private Bitmap bmp;

        public static IplBitmapComposite From(Bitmap bmp)
        {
            return new IplBitmapComposite(bmp);

        }

        public static IplBitmapComposite From(IplImage ipl)
        {
            return new IplBitmapComposite(ipl);
        }


        public System.Drawing.Bitmap Bmp
        {
            get 
            {
                if (this.bmp == null)
                {
                    this.bmp = BitmapConverter.ToBitmap(this.ipl);
                }
                return bmp; 
            }
        }
        
        public IplImage Ipl
        {
            get
            {
                if (this.ipl == null)
                {
                    this.ipl = BitmapConverter.ToIplImage(this.bmp);
                }
                return ipl;
            }
        }

        protected IplBitmapComposite(Bitmap bmp)
        {
            this.bmp = bmp;

        }

        protected IplBitmapComposite(IplImage ipl)
        {
            this.ipl = ipl;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (this.bmp != null) this.bmp.Dispose();
            if (this.ipl != null) this.ipl.Dispose();
        }

        #endregion
    }
}
