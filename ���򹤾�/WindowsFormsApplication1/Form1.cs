using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private List<Rectangle> _rectangles = new List<Rectangle>();

        public Form1()
        {
            InitializeComponent();
        }


        private System.Threading.Tasks.Task _t;

        private void button1_Click(object sender, EventArgs e)
        {
            _rectangles.Clear();
            this.Invalidate();

            //if (_t == null)
            //{
            //    _t = System.Threading.Tasks.Task.Factory.StartNew(() =>
            //                                                         {
            //                                                             while (true)
            //                                                             {
            //                                                                 var files =
            //                                                                 System.IO.Directory.EnumerateFiles(
            //                                                                     @"D:\testpic");
            //                                                             foreach (var file in files)
            //                                                             {
            //                                                                 var img =
            //                                                                     System.Drawing.Image.FromFile(file);

            //                                                                 _image = img;
            //                                                                 this.Invalidate();

            //                                                                 System.Threading.Thread.Sleep(500);
            //                                                             }
            //                                                             }

            //                                                         });

            //}

        }

        void camera_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            this.Invalidate();

            //_cam.SignalToStop();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Z))
            {
                if (_rectangles.Count > 0)
                {
                    _rectangles.RemoveAt(_rectangles.Count - 1);
                    this.Invalidate();
                }

                return true;
            }

           

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void pictureBox1_FigureDrawn(object sender, Damany.Windows.Form.DrawFigureEventArgs e)
        {
            pictureBox1.Clear();
            pictureBox1.AddRectangle(e.Rectangle);
        }



    }
}
