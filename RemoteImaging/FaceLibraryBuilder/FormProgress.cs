using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FaceLibraryBuilder
{
    public partial class FormProgress : Form
    {
        public FormProgress()
        {
            InitializeComponent();
        }

       

        private void FormProgress_Load(object sender, EventArgs e)
        {

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += this.UpdateFaceSample;
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync();

            Cursor.Current = Cursors.WaitCursor;
            
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Cursor.Current = Cursors.Default;

            this.Close();

            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("特征库生成完毕", "成功", 
                 MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }


        private int GetFaceSamplesCount()
        {
            string[] faceSamples = System.IO.Directory.GetFiles(Properties.Settings.Default.FaceSampleLib, "*.jpg");
            return faceSamples.Length;
        }


        private void UpdateFaceSample(object sender, DoWorkEventArgs args)
        {
            //训练 重新生成 人脸库
            FaceRecognition.FaceRecognizer.FaceTraining(100, 100, 40);
        }

    }
}
