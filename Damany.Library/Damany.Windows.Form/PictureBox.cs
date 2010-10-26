using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Damany.Windows.Form
{
    public class DrawFigureEventArgs : EventArgs
    {
        public Rectangle Rectangle { get; set; }
    }

    public partial class PictureBox : UserControl
    {
        private Image _image;
        private bool _drawRectangle;

        private double _scaleRatio = 1;
        private float _positionX;
        private float _positionY;
        private float _zoom = 1;

        private bool _isDrag;
        private Point _startPoint = Point.Empty;
        private Point _lastPoint = Point.Empty;

        private Rectangle _drawingRectangle = Rectangle.Empty;
        private List<Rectangle> _rectangles = new List<Rectangle>();



        public bool DrawRectangle
        {
            get { return _drawRectangle; }
            set
            {
                if (_drawRectangle == value)
                {
                    return;
                }

                _drawRectangle = value;
                this.Invalidate();
            }
        }

        public Image Image
        {
            get { return _image; }
            set
            {
                var old = _image;
                if (old != null)
                {
                    old.Dispose();
                }

                _image = value;
                Invalidate();
            }
        }

        public event EventHandler<DrawFigureEventArgs> FigureDrawn;

        public void RaiseFigureDrawn(DrawFigureEventArgs e)
        {
            EventHandler<DrawFigureEventArgs> handler = FigureDrawn;
            if (handler != null) handler(this, e);
        }

        public PictureBox()
        {
            InitializeComponent();

            SetStyle(
                     ControlStyles.ResizeRedraw |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.DoubleBuffer,
                     true);

            this.MouseDown += UserControl_MouseDown;
            this.MouseMove += UserControl_MouseMove;
            this.MouseUp += UserControl_MouseUp;
            this.Paint += UserControl_Paint;

            this.DragDrop += UserControl_DragDrop;
            this.DragOver += UserControl_DragOver;
        }


        public void Clear()
        {
            this._rectangles.Clear();
            this.Invalidate();
        }


        public void AddRectangle(Rectangle rectangle)
        {
            _rectangles.Add(rectangle);
            this.Invalidate();
        }

        void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            var imgW = _image == null ? 0 : _image.Width;
            var imgH = _image == null ? 0 : _image.Height;

            var worldPos = ScreenToWorld(e.Location);

            Text = string.Format("Img Size: {0}X{1}, Cur Pos: {2},{3}", imgW, imgH, worldPos.X, worldPos.Y);

            if (_isDrag)
            {
                _drawingRectangle.Width = e.X - _drawingRectangle.Left;
                _drawingRectangle.Height = e.Y - _drawingRectangle.Top;

                var inflated = _drawingRectangle;

                inflated.Inflate(50, 50);
                this.Invalidate(inflated);

                _lastPoint = e.Location;
            }
        }

        void UserControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isDrag && _lastPoint != _startPoint)
            {
                var recScreen = new Rectangle(_startPoint.X, _startPoint.Y, _lastPoint.X - _startPoint.X,
                                              _lastPoint.Y - _startPoint.Y);
                if (recScreen.Size.Height < 5 || recScreen.Size.Height < 5)
                {
                    return;
                }

                this.Invalidate();

                var rec = this.ScreenToWorld(recScreen);

                var arg = new DrawFigureEventArgs() { Rectangle = rec };
                this.RaiseFigureDrawn(arg);
            }

            _isDrag = false;
            _startPoint = Point.Empty;
            _lastPoint = Point.Empty;

        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (_image == null) return;

            _scaleRatio = Math.Max((double)_image.Width / this.ClientSize.Width, (double)_image.Height / this.ClientSize.Height);

            _zoom = 1.0f / (float)_scaleRatio;

            _positionX = (float)(this.ClientSize.Width * _scaleRatio / 2 - _image.Width / 2);
            _positionY = (float)(this.ClientSize.Height * _scaleRatio / 2 - _image.Height / 2);
            var mx = new Matrix(_zoom, 0, 0, _zoom, 0, 0);
            mx.Translate(_positionX, _positionY);
            e.Graphics.Transform = mx;
            e.Graphics.DrawImage(_image, new Rectangle(0, 0, _image.Width, _image.Height));

        }

        void UserControl_MouseDown(object sender, MouseEventArgs e)
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

        private void UserControl_Paint(object sender, PaintEventArgs e)
        {
            if (_image == null)
            {
                return;
            }


            e.Graphics.Transform = new Matrix();
            var color = Color.FromArgb(125, Color.Yellow);

            if (DrawRectangle)
            {
                _rectangles.ForEach(r =>
                {
                    var t = WorldToScreen(r);
                    DrawMarkedRectangle(e.Graphics, this.Font, new Pen(color, 3), t, r);
                });

            }

            if (_isDrag)
            {
                e.Graphics.Transform = new Matrix();
                DrawMarkedRectangle(e.Graphics, this.Font, Pens.Yellow, _drawingRectangle, ScreenToWorld(_drawingRectangle));
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

        private static void DrawMarkedRectangle(Graphics g, Font font, Pen pen, RectangleF rectangle, RectangleF worldRectangle)
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

            DrawStringWithTrans(g, locString, font, Brushes.Yellow, recOfLocation, true);

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

        protected Rectangle WorldToScreen(Rectangle rectangle)
        {
            var start = this.WorldToScreen(rectangle.Location);
            var end = this.WorldToScreen(new Point(rectangle.Right, rectangle.Bottom));

            var rec = new Rectangle(start, new Size(end.X - start.X, end.Y - start.Y));
            return rec;
        }

        protected Point WorldToScreen(Point pt)
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



        protected Point ScreenToWorld(Point point)
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

        protected Rectangle ScreenToWorld(Rectangle rectangle)
        {
            var start = ScreenToWorld(rectangle.Location);

            var endpoint = new Point(rectangle.Right, rectangle.Bottom);
            var tracked = ScreenToWorld(endpoint);

            return new Rectangle(start.X, start.Y, tracked.X - start.X, tracked.Y - start.Y);
        }


        private void UserControl_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;

        }

        private void UserControl_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                var file = files[0];

                try
                {
                    var img = Image.FromFile(files[0]);
                    _image = img;
                    this.Text = "Image Size: " + img.Size.ToString();

                    this.Invalidate();
                }
                catch
                {
                    MessageBox.Show(this, "无法打开文件", "PictureBox", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }

        }
    }
}
