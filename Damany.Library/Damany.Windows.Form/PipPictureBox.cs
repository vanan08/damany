using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Damany.Drawing;

namespace Damany.Windows.Form
{
    public class PipPictureBox : System.Windows.Forms.PictureBox
    {
        public PipPictureBox()
        {
            this.ResizeRedraw = true;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.SmallImageSizePercentage = 0.25f;
        }

        public Image SmallImage 
        { 
            get
            {
                return this.smallImg;

            }
            set
            {
                if (this.smallImg != null)
                {
                    this.smallImg.Dispose();
                }

                this.smallImg = value;
                this.Invalidate();
            }
        }


        public string Text { get; set; }
        public float SmallImageSizePercentage { get; set; }
        public bool DrawFrame { get; set; }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);


            if (this.SmallImage != null)
            {
                this.DrawSmallImage(e.Graphics);
            }

            if (!string.IsNullOrEmpty(this.Text))
            {
                this.DrawText(e.Graphics);
            }
        }

        private void DrawSmallImage(Graphics g)
        {
            var smallPicHeight = this.Height * this.SmallImageSizePercentage ;

            var x = this.ClientSize.Width - smallPicHeight;
            var y = this.ClientSize.Height - smallPicHeight;

            g.DrawImage(this.SmallImage, x, y, smallPicHeight, smallPicHeight);
        }

        private void DrawText(Graphics g)
        {
            using (var font = new Font(FontFamily.GenericSansSerif, 20))
            {
                g.DrawStringOutLined(this.Text, font, 0, 0);
            }
        }

        private Image smallImg;

    }
}
