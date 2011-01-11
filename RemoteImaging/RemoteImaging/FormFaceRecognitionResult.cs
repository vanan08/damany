using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RemoteImaging
{
    public partial class FormFaceRecognitionResult : Form
    {
        public FormFaceRecognitionResult()
        {
            InitializeComponent();
        }

        private void FaceRecognitionResult_Load(object sender, EventArgs e)
        {
            this.timer1.Tick += new EventHandler(timer1_Tick);
            this.timer1.Enabled = true;
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FaceRecognitionResult_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.capturedFace.Image != null)
            {
                this.capturedFace.Image.Dispose();
            }

            if (this.faceInLibrary.Image != null)
            {
                this.faceInLibrary.Image.Dispose();
            }
        }
    }
}
