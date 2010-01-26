using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace RemoteImaging.RealtimeDisplay
{
    public partial class Monitoring : Form
    {
        public Monitoring()
        {
            InitializeComponent();
            
        }
        string filePath = "";
        Point startPoint = new Point();
        Point endPoint = new Point();
        double widthPerc = 0;//比例
        double heightPerc = 0;
        bool hasMouse = false;
        bool trueClick = false;//clicked button "框背景"
        string listPointStr ="";//Save the coordinates for string 

        private void btnBrowserImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                //ofd.InitialDirectory = Application.StartupPath;
                ofd.RestoreDirectory = true;
                ofd.Filter = "Jpeg 文件|*.jpg";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    filePath = ofd.FileName;
                    SuofangImage();
                }
            }
        }
        protected void SuofangImage()
        {
            Bitmap bmap = new Bitmap(filePath);
            Image img = (Image)bmap;
            widthPerc = img.Width * 1.0 / pictureBox1.Width; //宽的比例
            heightPerc = img.Height * 1.0 / pictureBox1.Height; //长的比例
            Bitmap b = new Bitmap(pictureBox1.Width, pictureBox1.Height);//缩放
            Graphics g = Graphics.FromImage(b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(img, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height), new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
            pictureBox1.Image = (Image)b;
            g.Dispose();
            bmap.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            trueClick = true;
        }

        //control's image whether is null
        protected bool chechLiveImg()
        {
            return (pictureBox1.Image != null) ? true : false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listPointStr!="")
            {
                string[] strPoints = listPointStr.Split(' ');
                int oPointx = Convert.ToInt32(strPoints[0]);
                int oPointy = Convert.ToInt32(strPoints[1]);
                int tPointx = Convert.ToInt32(strPoints[2]);
                int tPointy = Convert.ToInt32(strPoints[3]);
                if ((System.Math.Abs( oPointx- tPointx) > 2)
                    && (System.Math.Abs(oPointy - tPointy) > 2))
                {
                    if (chechLiveImg())
                    {
                        Program.motionDetector.SetAlarmArea(oPointx, oPointy, tPointx, tPointy, checkBox1.Checked ? true : false);
                        Properties.Settings.Default.Point = listPointStr;
                        //Properties.Settings.Default.Save();
                    }
                }
                else
                {
                    MessageBox.Show("布防的范围至少为2x2!", "警告");
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (chechLiveImg() && trueClick)
            {
                startPoint.X = e.Location.X;
                startPoint.Y = e.Location.Y;
                endPoint.X = -1;
                endPoint.Y = -1;
                hasMouse = true;
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            if (trueClick)
                Cursor = Cursors.Cross;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (trueClick)
                Cursor = Cursors.Arrow;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

            Point ptCurrent = new Point(e.Location.X, e.Location.Y);
            if (hasMouse)
            {
                if (((ptCurrent.X <= pictureBox1.Width - 1) && (ptCurrent.X >= 0)) && ((ptCurrent.Y <= pictureBox1.Height - 1) && (ptCurrent.Y >= 0)))
                {
                    if (endPoint.X != -1)
                    {
                        MyDrawReversibleRectangle(startPoint, endPoint);
                    }
                    endPoint = ptCurrent;
                    MyDrawReversibleRectangle(startPoint, ptCurrent);
                }
            }
        }

        private void MyDrawReversibleRectangle(Point p1, Point p2)
        {
            Rectangle rc = new Rectangle();

            p1 = PointToScreen(p1);
            p2 = PointToScreen(p2);



            if (p1.X < p2.X)
            {
                rc.X = p1.X;
                rc.Width = p2.X - p1.X;
            }
            else
            {
                rc.X = p2.X;
                rc.Width = p1.X - p2.X;
            }
            if (p1.Y < p2.Y)
            {
                rc.Y = p1.Y;
                rc.Height = p2.Y - p1.Y;
            }
            else
            {
                rc.Y = p2.Y;
                rc.Height = p1.Y - p2.Y;
            }

            ControlPaint.DrawReversibleFrame(rc,
                            Color.Black, FrameStyle.Thick);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (chechLiveImg() && trueClick)
            {
                endPoint.X = e.Location.X;
                endPoint.Y = e.Location.Y;

                if ((startPoint.X > endPoint.X) || (startPoint.Y > endPoint.Y))
                {
                    Point temp = new Point();
                    temp = startPoint;
                    startPoint = endPoint;
                    endPoint = temp;
                    if (startPoint.X < 0) startPoint.X = 0;
                    if (startPoint.Y < 0) startPoint.Y = 0;
                }
                else
                {
                    if (endPoint.X > pictureBox1.Width) endPoint.X = pictureBox1.Width;
                    if (endPoint.Y > pictureBox1.Height) endPoint.Y = pictureBox1.Height;
                }
             
                listPointStr = GetContent();

                hasMouse = false;

                endPoint.X = 0;
                endPoint.Y = 0;
                startPoint.X = -1;
                startPoint.Y = -1;
            }
        }

        protected string GetContent()
        {
            startPoint.X = Convert.ToInt32(startPoint.X * widthPerc);
            startPoint.Y = Convert.ToInt32(startPoint.Y * heightPerc);
            endPoint.X = Convert.ToInt32(endPoint.X * widthPerc);
            endPoint.Y = Convert.ToInt32(endPoint.Y * heightPerc);
            return startPoint.X + " " + startPoint.Y + " " + endPoint.X + " " + endPoint.Y + " ";
        }

        [DllImport("User32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(int hwnd, int msg, int wparam, int lparam);

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
            listPointStr = "";
            #region
            ////SendMessage(pictureBox1.Handle.ToInt32(), 0x304, 0, 0);
            //if ((listPointStr != null) && (listPointStr.Count >= 1))
            //{
            //    listPointStr.RemoveAt(listPointStr.Count - 1);
            //    if (listPointStr.Count == 0)
            //    {
            //        SuofangImage();
            //    }
            //    else
            //    {
            //        SuofangImage();
            //        foreach (string str in listPointStr)
            //        {
            //            string[] arrPoint = str.Split(' ');
            //            Point sPoint = new Point(Convert.ToInt32(Convert.ToInt32(arrPoint[0]) / widthPerc), Convert.ToInt32(Convert.ToInt32(arrPoint[1]) / heightPerc));
            //            Point ePoint = new Point(Convert.ToInt32(Convert.ToInt32(arrPoint[2]) / widthPerc), Convert.ToInt32(Convert.ToInt32(arrPoint[3]) / heightPerc));

            //            Graphics grah = Graphics.FromImage(pictureBox1.Image);
            //            grah.DrawRectangle(Pens.Red,new Rectangle(sPoint.X,sPoint.Y,ePoint.X-sPoint.X,ePoint.Y-sPoint.Y));
            //            grah.Dispose();
            //        }
            //    }
            //}
            #endregion
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}