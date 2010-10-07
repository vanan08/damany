using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Damany.Windows.Form
{
    public delegate void CellDoubleClickHandler(object sender, CellDoubleClickEventArgs args);

    public partial class SquareListView : UserControl
    {
        private object imgQueueLocker = new object();

        public SquareListView()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            refreshTimer = new Timer();
            refreshTimer.Interval = 50;
            refreshTimer.Tick += refreshTimer_Tick;

            this.AutoDisposeImage = true;

            this.MaxCountOfCells = 500;
            this.cells = new List<Cell>(this.MaxCountOfCells);
            this.numOfColumns = 2;
            this.numOfRows = 2;


            this.PopulateCellList();
            this.CalcLayout();

            this.Resize += SquareListView_Resize;
            this.MouseDoubleClick += new MouseEventHandler(SquareListView_MouseDoubleClick);

            this.PaddingChanged += (sender, args) => this.CalcLayout();

            this.LastSelectedCell = Cell.Empty;
        }

        void SquareListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Cell c = this.CellFromPoint(e.Location);

            if (c == null)
            {
                return;
            }

            this.FireCellDoubleClick(c);

        }

        private void validateIndexRange(int idx)
        {
            if (idx > this.CellsCount - 1)
                throw new System.IndexOutOfRangeException();
        }

        public Image this[int idx]
        {
            get
            {
                validateIndexRange(idx);
                return this.cells[idx].Image;
            }

            set
            {
                validateIndexRange(idx);
                this.cells[idx].Image = value;
            }

        }




        private void FireSelectedCellChanged()
        {
            if (SelectedCellChanged != null)
            {
                SelectedCellChanged(this, EventArgs.Empty);
            }
        }

        private void FireCellDoubleClick(Cell c)
        {
            if (CellDoubleClick != null)
            {
                CellDoubleClickEventArgs arg = new CellDoubleClickEventArgs();
                arg.Cell = new ImageCell()
                {
                    Image = c.Image,
                    Path = c.Path,
                    Text = c.Text,
                    Tag = c.Tag,

                };

                CellDoubleClick(this, arg);
            }
        }




        private void ClearPrevCell()
        {
            int prevIdx = this.cursor == 0 ? this.CellsCount - 1 : this.cursor - 1;

            Cell c = this.cells[prevIdx];
            c.HightLight = false;
            this.Invalidate(c.Bound.ToRectangle());
        }

        void refreshTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                ImageCell imgToShow = null;
                lock (this.imgQueueLocker)
                {
                    if (imgQueue.Count <= 0)
                    {
                        this.refreshTimer.Enabled = false;
                        return;
                    }
                    else
                    {
                        imgToShow = imgQueue.Dequeue();
                    }
                }

                RepositionCursor();

                ClearPrevCell();
                Cell dstCell = this.cells[cursor];

                if (this.AutoDisposeImage && dstCell.Image != null)
                {
                    dstCell.Image.Dispose();

                    var disposable = dstCell.Tag as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }

                dstCell.Image = imgToShow.Image;
                dstCell.Text = imgToShow.Text;
                dstCell.Path = imgToShow.Path;
                dstCell.Tag = imgToShow.Tag;
                dstCell.HightLight = true;

                this.Invalidate(dstCell.Bound.ToRectangle());
                cursor++;
            }
            catch (InvalidOperationException)// the queue is empty
            {
                this.refreshTimer.Enabled = false;
            }

            System.Diagnostics.Debug.WriteLine("tick");

        }

        public bool AutoDisposeImage { get; set; }


        private void EnableTimer(bool enable)
        {
            Action<bool> ac = e => refreshTimer.Enabled = e;

            if (this.InvokeRequired)
            {
                this.BeginInvoke(ac, enable);
            }
            else
            {
                refreshTimer.Enabled = true;

            }

        }
        public void ShowImages(ImageCell[] imgs)
        {
            lock (this.imgQueueLocker)
            {
                Array.ForEach(imgs, imgQueue.Enqueue);

                if (imgQueue.Count > 0 && this.Visible)
                {
                    EnableTimer(true);
                    System.Diagnostics.Debug.WriteLine("tick");
                }
            }

        }



        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }


        private void CalcLayout()
        {

            float width = (float)this.ClientRectangle.Width / this.NumberOfColumns;
            float height = (float)this.ClientRectangle.Height / this.NumberofRows;

            for (int i = 0; i < this.numOfRows; i++)
            {
                for (int j = 0; j < this.numOfColumns; j++)
                {
                    int idx = j + i * this.numOfColumns;
                    this.cells[idx].Bound = new RectangleF(j * width, i * height, width, height);
                    this.cells[idx].Rec = new RectangleF(j * width + this.Padding.Left,
                        i * height + this.Padding.Top,
                        width - this.Padding.Horizontal,
                        height - this.Padding.Vertical);

                    this.cells[idx].Column = j;
                    this.cells[idx].Row = i;
                    this.cells[idx].Index = idx;
                    this.cells[idx].Font = this.Font;
                }
            }

            this.Invalidate();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            for (int i = 0; i < this.CellsCount; i++)
            {
                Cell c = this.cells[i];
                if (e.ClipRectangle.IntersectsWith(new Rectangle((int) c.Bound.X, (int) c.Bound.Y, (int) c.Bound.Width, (int) c.Bound.Height)))
                {
                    c.Paint(e.Graphics);
                }
            }

            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);

        }

        private Cell CellFromPoint(Point pt)
        {
            foreach (Cell c in this.cells)
            {
                if (c.Bound.Contains(pt))
                {
                    return c;
                }
            }

            return null;
        }

        private void PaintSelectedCell(Cell c)
        {
            LastSelectedCell.Selected = false;
            this.Invalidate(LastSelectedCell.Bound.ToRectangle());

            c.Selected = true;
            this.Invalidate(c.Bound.ToRectangle());
        }


        private void SquareListView_MouseClick(object sender, MouseEventArgs e)
        {
            Cell c = CellFromPoint(e.Location);
            if (c != null)
            {
                this.SelectedCell = c;
            }
        }

        void PopulateCellList()
        {
            int length = this.MaxCountOfCells;
            for (int i = 0; i < length; i++)
            {
                cells.Add(new Cell());
            }
        }

        void SquareListView_Resize(object sender, EventArgs e)
        {
            var c = this.ClientSize.Width/64.0;
            if (c <= 0)
            {
                c = 1;
            }

            this.NumberOfColumns = (int) c;

            var r = this.ClientSize.Height/80.0;
            if (r <= 0)
            {
                r = 1;
            }
            this.NumberofRows = (int) r;
        }

        void RepositionCursor()
        {
            if (cursor > this.CellsCount - 1)
            {
                cursor = 0;
            }
        }


        public int NumberofRows
        {
            get
            {
                return numOfRows;
            }
            set
            {
                if (numOfRows == value)
                    return;

                numOfRows = value;

                if (this.SelectedCell != null)
                {
                    this.SelectedCell.Selected = false;
                }

                this.CalcLayout();
                this.Invalidate();
            }
        }


        public int NumberOfColumns
        {
            get
            {
                return numOfColumns;
            }
            set
            {
                if (numOfColumns == value)
                    return;


                numOfColumns = value;

                if (this.SelectedCell != null)
                {
                    this.SelectedCell.Selected = false;
                }

                this.CalcLayout();
                this.Invalidate();
            }
        }


        public Cell HitTest(Point pt)
        {
            return CellFromPoint(pt);
        }

        public Cell HitTest(int x, int y)
        {
            return CellFromPoint(new Point(x, y));
        }




        private Cell _SelectedCell;
        public Cell SelectedCell
        {
            get
            {
                return _SelectedCell;
            }
            set
            {
                if (_SelectedCell == value)
                    return;

                _SelectedCell = value;

                this.PaintSelectedCell(value);

                FireSelectedCellChanged();

                LastSelectedCell = value;
            }
        }


        public Cell LastSelectedCell { get; private set; }


        public int CellsCount { get { return this.numOfColumns * this.numOfRows; } }
        public int MaxCountOfCells { get; set; }


        public event EventHandler SelectedCellChanged;
        public event CellDoubleClickHandler CellDoubleClick;


        int cursor = 0;
        IList<Cell> cells;
        Timer refreshTimer;
        Queue<ImageCell> imgQueue = new Queue<ImageCell>();
        private int numOfColumns;
        private int numOfRows;
    }
}
