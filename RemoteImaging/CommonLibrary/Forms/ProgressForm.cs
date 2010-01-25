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
        public ProgressForm()
        {
            InitializeComponent();
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
    }
}
