using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kise.IdCard.UI
{
    public partial class FormSettings : DevExpress.XtraEditors.XtraForm
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
