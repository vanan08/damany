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

        public SuspectsRepository.SuspectsRepositoryManager Manager { get; set; }
       

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


        private void UpdateFaceSample(object sender, DoWorkEventArgs args)
        {
            this.Manager.UpdateRepository();
        }

    }
}
