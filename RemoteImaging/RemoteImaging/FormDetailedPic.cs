using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RemoteImaging
{
    using Core;
    using AForge.Imaging.Filters;

    public partial class FormDetailedPic : Form
    {
        public FormDetailedPic()
        {
            InitializeComponent();
        }


        ImageDetail imgDetail;

        public ImageDetail Img
        {
            set
            {
                byte[] buff = File.ReadAllBytes(value.Path);
                this.pictureEdit1.Image = Image.FromStream(new MemoryStream(buff));
                this.imgDetail = value;
                this.captureTime.Text = value.CaptureTime.ToString();
            }
        }

        private void FormDetailedPic_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.pictureEdit1.Dispose();
        }

        private void Brightness_Click(object sender, EventArgs e)
        {
            IPLab.BrightnessForm frm = new IPLab.BrightnessForm();
            frm.Image = (Bitmap)this.pictureEdit1.Image;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.ApplyFilter(frm.Filter);
            }

            frm.Dispose();
        }

        Image backup;

        private void ApplyFilter(IFilter filter)
        {
            try
            {
                // set wait cursor
                this.Cursor = Cursors.WaitCursor;

                // apply filter to the image
                Bitmap newImage = filter.Apply((Bitmap)this.pictureEdit1.Image);

                if (backup == null)
                {
                    backup = this.pictureEdit1.Image;
                }

                this.pictureEdit1.Image = newImage;
                this.Save.Enabled = true;
                this.Restore.Enabled = true;


            }
            catch (ArgumentException)
            {
                MessageBox.Show("Selected filter can not be applied to the image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // restore cursor
                this.Cursor = Cursors.Default;
            }
        }

        private void Sharpen_Click(object sender, EventArgs e)
        {
            this.ApplyFilter(new Sharpen());
        }

        private void Restore_Click(object sender, EventArgs e)
        {
            if (backup != null)
            {
                this.pictureEdit1.Image = backup;
            }
        }



        private void Save_Click(object sender, EventArgs e)
        {
            DialogResult result
                = MessageBox.Show(this,
                "确定要保存所做的修改?",
                "请确认",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.OK)
            {
                return;
            }

            this.pictureEdit1.Image.Save(this.imgDetail.Path);

        }
    }
}
