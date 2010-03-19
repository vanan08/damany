﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RemoteImaging.Query
{
    public partial class FaceCompare : Form
    {
        public FaceCompare()
        {
            InitializeComponent();

            this.searchFrom.EditValue = DateTime.Now.AddDays(-1);
            this.searchTo.EditValue = DateTime.Now;

            this.targetPic.Paint += new PaintEventHandler(targetPic_Paint);
            this.compareButton.Click += new EventHandler(compareButton_Click);
        }

        void compareButton_Click(object sender, EventArgs e)
        {
            this.presenter.CompareClicked();
        }

        public void AttachPresenter(FaceComparePresenter presenter)
        {
            this.presenter = presenter;
        }

        void targetPic_Paint(object sender, PaintEventArgs e)
        {
        }

        private void choosePic_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog(this) == DialogResult.Cancel)
            {
                return;
            }

            var img = Damany.Util.Extensions.MiscHelper.FromFileBuffered(this.openFileDialog1.FileName);
            this.targetPic.Image = img;

            this.ipl = OpenCvSharp.IplImage.FromBitmap((Bitmap)img);

            var searcher = new FaceSearchWrapper.FaceSearch();

            var frame = new Damany.Imaging.Common.Frame(ipl);
            frame.MotionRectangles.Add(new OpenCvSharp.CvRect(0, 0, ipl.Width, ipl.Height));
            searcher.AddInFrame(frame);
            var portraits = searcher.SearchFaces();
            var face = portraits.FirstOrDefault();

            if (face == null)
            {
                MessageBox.Show("未定位到人脸", this.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.faceRect = face.Portraits[0].FacesRectForCompare;

            this.targetPic.Invalidate();
        }

        public DateTime SearchFrom
        {
            get
            {
                return (DateTime)this.searchFrom.EditValue;
            }
        }

        public DateTime SearchTo
        {
            get
            {
                return (DateTime)this.searchTo.EditValue;
            }
        }

        public Image CurrentImage
        {
            set
            {
                if (InvokeRequired)
                {
                    Action<Image> action = img => this.CurrentImage = img;

                    this.BeginInvoke(action, value);
                    return;
                }


                if (this.currentPic.Image != null)
                {
                    this.currentPic.Image.Dispose();
                }

                this.currentPic.Image = value;
            }
        }

        public OpenCvSharp.IplImage Image
        {
            get
            {
                return this.ipl;
            }
        }

        public Rectangle FaceRect
        {
            get
            {
                return this.faceRect;
            }
        }

        public void ClearFaceList()
        {
            this.imageList1.Images.Clear();
        }

        public void AddPortrait(Damany.Imaging.Common.Portrait p)
        {
            if (this.InvokeRequired)
            {
                Action<Damany.Imaging.Common.Portrait> action
                    = this.AddPortrait;

                this.BeginInvoke(action, p);
                return;
            }

            this.imageList1.Images.Add(p.GetImage().ToBitmap());

            var item = new ListViewItem();
            item.Text = p.CapturedAt.ToString();
            item.ImageIndex = this.imageList1.Images.Count - 1;

            this.faceList.Items.Add(item);

            p.Dispose();
        }

        private Rectangle faceRect;
        private FaceComparePresenter presenter;
        private OpenCvSharp.IplImage ipl;

        private void button1_Click(object sender, EventArgs e)
        {
            this.presenter.CompareClicked();
        }


    }
}
