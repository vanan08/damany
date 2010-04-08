using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Damany.Drawing
{
    public static class GraphicsExtensions
    {
        public static void DrawStringOutLined(this Graphics g, string textToDraw, Font font,  float x, float y)
        {
            g.DrawString(textToDraw, font, Brushes.Black, x - 1, y);
            g.DrawString(textToDraw, font, Brushes.Black, x, y - 1);
            g.DrawString(textToDraw, font, Brushes.Black, x + 1, y);
            g.DrawString(textToDraw, font, Brushes.Black, x, y + 1);
            g.DrawString(textToDraw, font, Brushes.White, x, y);
        }

        public static void DrawStringInCenterOfRectangle(this Graphics g, string str, Font font, Brush brush, Rectangle rec)
        {
            SizeF sizeOfPrompt = g.MeasureString(str, font);
            float x = rec.X + (rec.Width - sizeOfPrompt.Width) / 2;
            float y = rec.Y + (rec.Height - sizeOfPrompt.Height) / 2;
            g.DrawString(str, font, brush, x, y);
        }
    }
}
