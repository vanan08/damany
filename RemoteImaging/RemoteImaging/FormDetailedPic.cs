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


        private void FormDetailedPic_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Image.Dispose();
        }



        Image backup;

        private void ApplyFilter(IFilter filter)
        {
            try
            {
                // set wait cursor
                this.Cursor = Cursors.WaitCursor;

                // apply filter to the image
                Bitmap newImage = filter.Apply((Bitmap)this.Image.Image);

                if (backup == null)
                {
                    backup = this.Image.Image;
                }

                this.Image.Image = newImage;


            }
            catch (ArgumentException)
            {
                MessageBox.Show("Selected filter can not be applied to the image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
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
                this.Image.Image = backup;
            }
        }

        private void Contrast_Click(object sender, EventArgs e)
        {
            IPLab.ContrastForm frm = new IPLab.ContrastForm();
            frm.Image = (Bitmap)this.Image.Image;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.ApplyFilter(frm.Filter);
            }

            frm.Dispose();
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

            this.Image.Image.Save(this.imgDetail.Path);

        }
    }
}
