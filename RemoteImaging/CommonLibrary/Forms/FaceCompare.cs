using System;
using System.Drawing;
using System.Windows.Forms;
using Damany.RemoteImaging.Common.Presenters;
using Damany.Imaging.Extensions;
using FaceSearchWrapper;

namespace Damany.RemoteImaging.Common.Forms
{
    public partial class FaceCompare : Form
    {
        private readonly ConfigurationManager _manager;

        public FaceCompare()
        {
            InitializeComponent();

            this.searchFrom.EditValue = DateTime.Now.AddDays(-1);
            this.searchTo.EditValue = DateTime.Now;

            this.targetPic.Paint += targetPic_Paint;
            this.compareButton.Click += compareButton_Click;
        }

        public FaceCompare(ConfigurationManager manager)
            : this()
        {
            _manager = manager;
        }

        public void EnableStartButton(bool enable)
        {
            if (this.InvokeRequired)
            {
                Action<bool> action = this.EnableStartButton;
                this.BeginInvoke(action, enable);
                return;
            }

            this.compareButton.Text = enable ? "比对" : "停止";
        }

        void compareButton_Click(object sender, EventArgs e)
        {
            presenter.CompareClicked();

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

            var rects = this.ipl.LocateFaces(this.searcher);

            if (rects.Length == 0)
            {
                MessageBox.Show("未定位到人脸", this.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.faceRect = rects[0];

            this.targetPic.Invalidate();
            this.compareButton.Enabled = true;
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
            this.faceList.Items.Clear();
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

            this.imageList1.Images.Add(p.GetIpl().ToBitmap());

            var item = new ListViewItem();
            item.Text = (_manager.GetName(p.CapturedFrom.Id) ?? string.Empty) + " " + p.CapturedAt.ToString();
            item.ImageIndex = this.imageList1.Images.Count - 1;

            this.faceList.Items.Add(item);

            p.Dispose();
        }


        public void SetStatusText(string msg)
        {
            if (InvokeRequired)
            {
                Action<string> action = SetStatusText;
                this.BeginInvoke(action, msg);
                return;
            }

            this.toolStripStatusLabel1.Text = msg;
        }

        public CompareAccuracy SelectedAccuracy
        {
            get
            {
                return (CompareAccuracy) this.radioGroup1.SelectedIndex;
            }
            set
            {
                int idx = (int) value;
                this.radioGroup1.SelectedIndex = idx;
            }
        }



        private void FaceCompare_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.presenter.Stop();
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.presenter.ThresholdChanged();
        }

        private Rectangle faceRect;
        private FaceComparePresenter presenter;
        private OpenCvSharp.IplImage ipl;
        private bool started;
        private FaceSearchWrapper.FaceSearch searcher = new FaceSearch();
    }
}
