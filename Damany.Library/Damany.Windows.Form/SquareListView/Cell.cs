using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Damany.Drawing;

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
        public string OverlayText { get; set; }
        public bool EnableOverlayText { get; set; }
        public Font Font { get; set; }


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

        private void DrawOverlayText(Graphics g, Rectangle rectangle)
        {
            if (this.EnableOverlayText && !string.IsNullOrEmpty(this.OverlayText))
            {
                using (Font font = new Font(FontFamily.GenericSansSerif, this.Font.Size + 5, FontStyle.Bold))
                {
                    g.DrawStringOutLined(this.OverlayText, font, rectangle.Left+3, rectangle.Top+3);
                }
            }
        }

        private void DrawHiliteBackground(Graphics g)
        {
            if (this.HightLight && !this.Selected)
            {
                g.FillRectangle(Brushes.LightBlue, this.Rec);
            }
        }
        public void Paint(Graphics g)
        {
            g.FillRectangle(SystemBrushes.Control, this.Bound);

            DrawHiliteBackground(g);

            if (this.Selected)
            {
                g.FillRectangle(Brushes.DarkBlue, this.Rec);
            }

            if (this.Image == null)
            {
                g.DrawStringInCenterOfRectangle("未指定图片",
                                                this.Font,
                                                this.Selected ? Brushes.White : SystemBrushes.ControlText,
                                                this.Rec);


                g.DrawRectangle(Pens.Gray, this.Rec);
            }
            else
            {
                SizeF sizeOfText = SizeF.Empty;
                int space = 3;

                Rectangle recOfImg = this.Rec;

                if (!string.IsNullOrEmpty(this.Text))
                {
                    sizeOfText = g.MeasureString(this.Text, this.Font);
                    Rectangle recText = this.Rec;
                    int h = this.Rec.Height - (int)sizeOfText.Height - space;
                    recText.Offset(0, h);
                    recText.Height -= h;

                    g.DrawStringInCenterOfRectangle(this.Text, this.Font, SystemBrushes.ControlText, recText);

                    recOfImg.Height -= (int)sizeOfText.Height + space;
                }


                Rectangle rectangleOfImagePart = CalculateAutoFitRectangle(
                                                            recOfImg,
                                                            new Rectangle(0, 0, this.Image.Width, this.Image.Height) );

                g.DrawImage(this.Image, rectangleOfImagePart);

                DrawOverlayText(g, rectangleOfImagePart);

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
