using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Damany.RemoteImaging.Common.Forms
{
    public partial class ProgressForm : Form, IProgress
    {
        public event DoWorkEventHandler DoWork
        {
            add
            {
                this.worker.DoWork += value;
            }
            remove
            {
                this.worker.DoWork -= value;
            }
        }

        public event RunWorkerCompletedEventHandler WorkIsDone
        {
            add
            {
                this.worker.RunWorkerCompleted += value;
            }
            remove
            {
                this.worker.RunWorkerCompleted -= value;
            }
        }


        public ProgressForm()
        {
            InitializeComponent();

            this.worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            this.worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;

            string status = e.UserState as string;
            if (status != null)
            {
                this.status.Text = status;
            }
        }


        #region IProgress Members

        public int Percent
        {
            set
            {
                if (this.InvokeRequired)
                {
                    Action<int> advance = SetProgress;
                    this.Invoke(advance, value);
                }
                else
                {
                    SetProgress(value);
                }
            }
        }

        private void SetProgress(int i)
        {
            this.progressBar1.Value = i;
            if (i >= this.progressBar1.Maximum)
            {
                this.Close();
            }
        }

        #endregion



        private void ProgressForm_Shown(object sender, EventArgs e)
        {
            this.worker.RunWorkerAsync();
        }
    }
}
