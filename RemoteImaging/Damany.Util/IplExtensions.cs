using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace Damany.Util
{
    public static class IplExtensions
    {
        public static void DrawRect(this IplImage ipl, CvRect rect, CvColor color, int thickNess)
        {
            var roi = ipl.ROI;
            ipl.ResetROI();
            ipl.DrawRect(rect.X, rect.Y, rect.X + rect.Width, rect.Y + rect.Height, color, thickNess);
            ipl.SetROI(roi);
        }

        public static IplImage CvtToGray(this IplImage ipl)
        {
            var gray = new IplImage(ipl.Width, ipl.Height, BitDepth.U8, 1);

            var roi = ipl.ROI;
            ipl.ResetROI();
            ipl.CvtColor(gray,ColorConversion.BgrToGray);
            ipl.SetROI(roi);

            gray.SetROI(roi);

            return gray;
        }
    }
}
