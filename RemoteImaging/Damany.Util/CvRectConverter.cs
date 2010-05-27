using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;
using System.Drawing;

namespace Damany.Util
{
    public static  class CvRectConverter
    {
        public static CvRect ToCvRect(this Rectangle rectangle)
        {
            return new CvRect(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height);
        }

        public static Rectangle ToRectangle(this CvRect rect)
        {
            return new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }
    }
}
