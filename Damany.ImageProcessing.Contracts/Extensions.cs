using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace Damany.Imaging.Contracts
{
    public static class Extensions
    {
        public static void Dispose(this IList<Frame> frames)
        {
            if (frames != null)
            {
                foreach (var item in frames)
                {
                    item.Dispose();
                }
            }
        }

        public static System.Drawing.Rectangle ToRectangle(this OpenCvSharp.CvRect rect)
        {
            return new System.Drawing.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);

        }

        public static OpenCvSharp.IplImage GetSub(this OpenCvSharp.IplImage ipl, OpenCvSharp.CvRect subRect)
        {
            if (ipl == null)
                throw new ArgumentNullException("ipl", "ipl is null.");

            if (!ipl.BoundingRect().Contains(subRect))
                throw new InvalidOperationException("subRect is outside of ipl");


            try
            {
                ipl.SetROI(subRect);

                OpenCvSharp.IplImage sub = new IplImage(
                    ipl.GetSize(),
                    ipl.Depth,
                    ipl.NChannels);

                ipl.Copy(sub);
                return sub;
            }
            finally
            {
                ipl.ResetROI();
            }
        }
    }

}
