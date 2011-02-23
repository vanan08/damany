using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FaceSearcherTester
{
    public partial class Form1 : Form
    {
        FaceSearchController _controller;


        public string Status
        {
            set { statusLabel.Text = value; }
        }

        public Image SearchResult
        {
            set { pictureBox1.Image = value; }
        }

        public int X
        {
            get { return int.Parse(x.Text); }
        }

        public int Y
        {
            get { return int.Parse(y.Text); }
        }

        public int W
        {
            get { return int.Parse(w.Text); }
        }

        public int H
        {
            get { return int.Parse(h.Text); }
        }



        public Form1()
        {
            InitializeComponent();

            _controller = new FaceSearchController(this);

            minFaceWidth.DataBindings.Add("Value", _controller, "MinFaceWidth");
            maxFaceWidth.DataBindings.Add("Value", _controller, "MaxFaceWidth");
            drawFaceSize.DataBindings.Add("Checked", _controller, "DrawFaceSize", true, DataSourceUpdateMode.OnPropertyChanged);
            applyROI.DataBindings.Add("Checked", _controller, "UseROI", true, DataSourceUpdateMode.OnPropertyChanged);

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

        private void x_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = CheckNumberInput(x);
        }

        private bool CheckNumberInput(TextBox textBox)
        {
            return applyROI.Checked && !IsNumber(textBox.Text);
        }
        private static bool IsNumber(string text)
        {
            if (string.IsNullOrEmpty(text)) return false;
            int number;
            if (!int.TryParse(text, out number)) return false;

            return true;
        }

        private void y_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = CheckNumberInput(y);
        }

        private void w_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = CheckNumberInput(w);
        }

        private void h_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = CheckNumberInput(h);
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            var r = new Rectangle(X, Y, W, H);
            _controller.Roi = r;
            _controller.SearchFace();
        }

    }
}
