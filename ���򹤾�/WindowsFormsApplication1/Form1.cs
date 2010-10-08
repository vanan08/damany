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
        private Image _image;
        private double _scaleRatio = 1;
        private float _positionX = 0;
        private float _positionY = 0;
        private float _zoom = 1;

        private bool _isDrag = false;
        private Point _startPoint = Point.Empty;
        private Point _lastPoint = Point.Empty;


        private Rectangle _drawingRectangle = Rectangle.Empty;
       

        private List<Rectangle> _rectangles = new List<Rectangle>();

        public Form1()
        {
            InitializeComponent();

            SetStyle(
        ControlStyles.ResizeRedraw |
        ControlStyles.AllPaintingInWmPaint |
        ControlStyles.DoubleBuffer,
        true);



            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);

            // _image = Image.FromFile("200630082214-331-988960.jpeg");

        }

        void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //this.mousePosition.Text = "Screen: " + e.Location + " World: " + BacktrackPoint(e.Location).ToString();



            if (_isDrag)
            {
                //var rectangle = new Rectangle(_startPoint.X, _startPoint.Y, _lastPoint.X - _startPoint.X,
                //                          _lastPoint.Y - _startPoint.Y);

                //rectangle = this.RectangleToScreen(rectangle);


                //ControlPaint.DrawReversibleFrame(rectangle, Color.Black, FrameStyle.Thick);

                //var rectangleNew = new Rectangle(_startPoint.X, _startPoint.Y, e.Location.X - _startPoint.X,
                //                              e.Location.Y - _startPoint.Y);
                //rectangleNew = RectangleToScreen(rectangleNew);

                //ControlPaint.DrawReversibleFrame(rectangleNew, Color.Black, FrameStyle.Thick);

                _drawingRectangle.Width = e.X - _drawingRectangle.Left;
                _drawingRectangle.Height = e.Y - _drawingRectangle.Top;

                System.Diagnostics.Debug.WriteLine(e.Location);

                var inflated = _drawingRectangle;

                inflated.Inflate(25, 25);
                this.Invalidate(inflated);

                _lastPoint = e.Location;
            }




        }

        void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isDrag && _lastPoint != _startPoint)
            {
                var recScreen = new Rectangle(_startPoint.X, _startPoint.Y, _lastPoint.X - _startPoint.X,
                                              _lastPoint.Y - _startPoint.Y);
                if (recScreen.Size.Height < 5 || recScreen.Size.Height  < 5)
                {
                    return;
                }

               var rec = this.BacktrackRectangle(recScreen);

                System.Diagnostics.Debug.WriteLine(rec.ToString());


               _rectangles.Add(rec);
                Invalidate();

            }

            _isDrag = false;
            _startPoint = Point.Empty;
            _lastPoint = Point.Empty;

        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if(_image == null) return;

            _scaleRatio = Math.Max((double)_image.Width / this.ClientSize.Width, (double)_image.Height / this.ClientSize.Height);

            _zoom = 1.0f / (float)_scaleRatio;

            _positionX = (float)(this.ClientSize.Width * _scaleRatio / 2 - _image.Width / 2);
            _positionY = (float)(this.ClientSize.Height * _scaleRatio / 2 - _image.Height / 2);
            var mx = new Matrix(_zoom, 0, 0, _zoom, 0, 0);
            mx.Translate(_positionX, _positionY);
            e.Graphics.Transform = mx;
            e.Graphics.DrawImage(_image, new Rectangle(0, 0, _image.Width, _image.Height));// .DrawImageUnscaled(_image, 0, 0));

        }

        void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            _isDrag = true;
            _startPoint = e.Location;
            _lastPoint = e.Location;

            _drawingRectangle.Location = e.Location;
            _drawingRectangle.Size = Size.Empty;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (_image == null)
            {
                return;
            }


            e.Graphics.Transform = new Matrix();
            var color = Color.FromArgb(125, Color.Yellow);

            //e.Graphics.DrawString("hello there", this.Font, new SolidBrush(Color.White), this.TrackPoint(_positionOfString) );

            if (showCoilToolStripMenuItem.Checked)
            {
                 _rectangles.ForEach(r =>
                                    {
                                        var t = TrackRectangle(r);
                                        DrawMarkedRectangle(e.Graphics, this.Font, new Pen(color, 3), t, r);
                                        
                                    });

            }

           
            if (_isDrag)
            {
                e.Graphics.Transform = new Matrix();
                DrawMarkedRectangle(e.Graphics, this.Font, Pens.Yellow, _drawingRectangle, BacktrackRectangle(_drawingRectangle));
            }
        }

        private static void DrawStringWithTrans(Graphics g, string s, Font font, Brush brush, RectangleF r, bool fill)
        {
            var transBlack = Color.FromArgb(125, Color.Black);
            using (var b = new SolidBrush(transBlack))
            {
                if (fill)
                {
                    g.FillRectangle(b, r);
                }
                
                var format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                g.DrawString(s, font, brush, r, format);
            }
        }

        private static void DrawMarkedRectangle(Graphics g, Font font,  Pen pen, RectangleF rectangle, RectangleF worldRectangle)
        {
            var sz = string.Format("{0}X{1}", worldRectangle.Size.Width, worldRectangle.Size.Height);
            DrawStringWithTrans(g, sz, font, Brushes.Yellow, rectangle, true);

            var locationFormat = "{0},{1}";
            var locString = string.Format(locationFormat, worldRectangle.Left, worldRectangle.Top);

            var sizeOfLocationString = g.MeasureString(locString, font);
            var l = rectangle.Location;
            l.Y -= sizeOfLocationString.Height;

            var recOfLocation = new RectangleF(l, sizeOfLocationString);
            recOfLocation.Offset(0, -1);

            DrawStringWithTrans(g, locString, font, Brushes.Yellow, recOfLocation, true );

            var locationRB = new PointF(rectangle.Right, rectangle.Bottom);
            var locationRBWorld = new PointF(worldRectangle.Right, worldRectangle.Bottom);

            var ptString = string.Format(locationFormat, locationRBWorld.X, locationRBWorld.Y);
            var size = g.MeasureString(ptString, font);

            locationRB.X -= size.Width;
            var rectangleOfRB = new RectangleF(locationRB, size);
            rectangleOfRB.Offset(0, 1);

            DrawStringWithTrans(g, ptString, font, Brushes.Yellow, rectangleOfRB, true);

            //g.DrawRectangle(pen, rectangle);

        }

        protected Rectangle TrackRectangle(Rectangle rectangle)
        {
            var start = this.TrackPoint(rectangle.Location);
            var end = this.TrackPoint(new Point(rectangle.Right, rectangle.Bottom));

            var rec = new Rectangle(start, new Size(end.X - start.X, end.Y - start.Y));
            return rec;
        }

        protected Point TrackPoint(Point pt)
        {
            //Creates the drawing matrix with the right zoom;
            var mx = new Matrix(_zoom, 0, 0, _zoom, 0, 0);
            //pans it according to the scroll bars
            mx.Translate(_positionX, _positionY);

            //uses it to transform the current mouse position
            Point[] pa = new Point[] { new Point(pt.X, pt.Y) };
            mx.TransformPoints(pa);
            return pa[0];
        }



        protected Point BacktrackPoint(Point point)
        {
            //Creates the drawing matrix with the right zoom;
            var mx = new Matrix(_zoom, 0, 0, _zoom, 0, 0);
            //pans it according to the scroll bars
            mx.Translate(_positionX, _positionY);
            //inverts it
            mx.Invert();
            //uses it to transform the current mouse position
            Point[] pa = new Point[] { new Point(point.X, point.Y) };
            mx.TransformPoints(pa);
            return pa[0];
        }

        protected Rectangle BacktrackRectangle(Rectangle rectangle)
        {
            var start = BacktrackPoint(rectangle.Location);

            var endpoint = new Point(rectangle.Right, rectangle.Bottom);
            var tracked = BacktrackPoint(endpoint);

            return new Rectangle(start.X, start.Y, tracked.X-start.X, tracked.Y-start.Y);
            

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
            if (_image != null)
            {
                _image.Dispose();
                _image = null;
            }

            this._image = eventArgs.Frame;
            this.Invalidate();

            //_cam.SignalToStop();
        }

        private void Form1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;

        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                var file = files[0];

                var ext = System.IO.Path.GetExtension(file).ToLower();
                var extensions = new[] { ".jpg", ".jpeg" };


                if (extensions.Contains(ext))
                {
                    var img = Image.FromFile(files[0]);
                    _image = img;
                    this.Text =  "Image Size: " + img.Size.ToString();

                    this.Invalidate();
                }


            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            this.Invalidate();
        }

       
    }
}
