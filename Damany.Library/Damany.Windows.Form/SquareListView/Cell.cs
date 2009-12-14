using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Damany.Windows.Form
{
    public class Cell
    {
        public Rectangle Rec { get; set; }
        public Rectangle Bound { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int Index { get; set; }
        public bool HightLight { get; set; }


        public static Cell Empty
        {
            get
            {
                return new Cell() { Rec = Rectangle.Empty };
            }
        }


        private Image _image;
        public Image Image
        {
            get
            {
                return _image;
            }
            set
            {
                Image i = _image;

                _image = value;

                if (i != null)
                {
                    i.Dispose();
                }
            }
        }


        public bool Selected { get; set; }
        public object Tag { get; set; }

        private string _ImagePath;
        public string Path
        {
            get
            {
                return _ImagePath;
            }
            set
            {
                if (_ImagePath == value)
                    return;
                _ImagePath = value;
            }
        }


        public string Text { get; set; }

        private void DrawStringInCenterOfRectangle(string str, Graphics g, Font font, Rectangle rec)
        {
            SizeF sizeOfPrompt = g.MeasureString(str, font);
            float x = rec.X + (rec.Width - sizeOfPrompt.Width) / 2;
            float y = rec.Y + (rec.Height - sizeOfPrompt.Height) / 2;
            Brush br = this.Selected ? Brushes.White : SystemBrushes.ControlText;
            g.DrawString(str, font, br, x, y);
        }


        public void CenterRectangleRelativeTo(Rectangle destRectangle, ref Rectangle srcRectangle)
        {
            int x = destRectangle.X + (destRectangle.Width - srcRectangle.Width) / 2;
            int y = destRectangle.Y + (destRectangle.Height - srcRectangle.Height) / 2;

            srcRectangle.Offset(x, y);
        }

        public Rectangle CalculateAutoFitRectangle(Rectangle destRectangle, Rectangle srcRectangle)
        {
            Rectangle resultRec = srcRectangle;

            //scale the rectangle
            if (srcRectangle.Width > destRectangle.Width
                || srcRectangle.Height > destRectangle.Height)
            {
                float xScale = (float)destRectangle.Width / srcRectangle.Width;
                float yScale = (float)destRectangle.Height / srcRectangle.Height;

                float scale = Math.Min(xScale, yScale);
                resultRec.Height = (int)(resultRec.Height * scale);
                resultRec.Width = (int)(resultRec.Width * scale);
            }

            CenterRectangleRelativeTo(destRectangle, ref resultRec);
            return resultRec;
        }

        public void Paint(Graphics g, Font font)
        {
            g.FillRectangle(SystemBrushes.Control, this.Bound);

            if (this.HightLight && !this.Selected)
            {
                g.FillRectangle(Brushes.LightBlue, this.Rec);
            }

            if (this.Selected)
            {
                g.FillRectangle(Brushes.DarkBlue, this.Rec);
            }

            if (this.Image == null)
            {
                DrawStringInCenterOfRectangle("未指定图片", g, font, this.Rec);
                g.DrawRectangle(Pens.Gray, this.Rec);
            }
            else
            {
                SizeF sizeOfText = SizeF.Empty;
                int space = 3;

                Rectangle recOfImg = this.Rec;

                if (!string.IsNullOrEmpty(this.Text))
                {
                    sizeOfText = g.MeasureString(this.Text, font);
                    Rectangle recText = this.Rec;
                    int h = this.Rec.Height - (int)sizeOfText.Height - space;
                    recText.Offset(0, h);
                    recText.Height -= h;
                    DrawStringInCenterOfRectangle(this.Text, g, font, recText);

                    recOfImg.Height -= (int)sizeOfText.Height + space;
                }


                g.DrawImage(this.Image,
                    CalculateAutoFitRectangle(recOfImg,
                    new Rectangle(0, 0, this.Image.Width, this.Image.Height)));

                g.DrawRectangle(Pens.Gray, recOfImg);
            }
        }

        public override int GetHashCode()
        {
            return this.Index;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (!(obj is Cell)) return false;

            return this.Index == (obj as Cell).Index;
        }
    }
}
