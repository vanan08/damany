using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FaceSearcherTester
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        FaceSearchController _controller = new FaceSearchController();

        public Form1()
        {
            InitializeComponent();

            pictureEdit1.DataBindings.Add("EditValue", _controller, "ResultImage");
            faceCount.DataBindings.Add("EditValue", _controller, "FaceCount");
            minFaceWidth.DataBindings.Add("EditValue", _controller, "MinFaceWidth");
            maxFaceWidth.DataBindings.Add("EditValue", _controller, "MaxFaceWidth");
            drawFaceSizeMark.DataBindings.Add("EditValue", _controller, "DrawFaceSize", true, DataSourceUpdateMode.OnPropertyChanged);


        }

        private void Form1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            var files = e.Data.GetData(DataFormats.FileDrop) as string[];

            if (files != null && files.Length > 0)
            {
                _controller.ImageToSearch = Image.FromFile(files[0]);
            }
        }

        private void minFaceWidth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var te = sender as DevExpress.XtraEditors.TextEdit;
                if (te != null)
                {
                    te.DoValidate();
                }

            }
        }

    }
}
