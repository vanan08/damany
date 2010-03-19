using System;
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
        }

        private void choosePic_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog(this) == DialogResult.Cancel)
            {
                return;
            }

            var img = Damany.Util.Extensions.MiscHelper.FromFileBuffered(this.openFileDialog1.FileName);
            this.targetPic.Image = img;

            var ipl = OpenCvSharp.IplImage.FromBitmap((Bitmap) img);

            var searcher = new FaceSearchWrapper.FaceSearch();

            var frame = new Damany.Imaging.Common.Frame(ipl);
            searcher.AddInFrame(frame);
            var portraits = searcher.SearchFaces();
            var face = portraits.FirstOrDefault();

            if (face == null)
            {
                MessageBox.Show("未定位到人脸");
                return;
            }



        }
    }
}
