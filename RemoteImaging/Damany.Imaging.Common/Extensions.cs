using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace Damany.Imaging.Common
{
    public static class Extensions
    {


        public static OpenCvSharp.IplImage GetSub(this OpenCvSharp.IplImage ipl, OpenCvSharp.CvRect subRect)
        {
            if (ipl == null)
                throw new ArgumentNullException("ipl", "ipl is null.");

            var boundingRect = new CvRect(0, 0, ipl.Width, ipl.Height);

            if (!boundingRect.Contains(subRect))
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
