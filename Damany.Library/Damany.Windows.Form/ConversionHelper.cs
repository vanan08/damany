using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Damany.Windows.Form
{
    public static class ConversionHelper
    {
        public static System.Drawing.Rectangle ToRectangle(this System.Drawing.RectangleF rectangleF)
        {
            return new Rectangle((int) rectangleF.X, (int) rectangleF.Y, (int) rectangleF.Width, (int) rectangleF.Height);
        }
    }
}
