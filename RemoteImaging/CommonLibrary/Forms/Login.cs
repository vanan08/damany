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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }


        public string UserName
        {
            get
            {
                return this.tbUsername.Text;
            }
        }

        public string Password
        {
            get
            {
                return this.tbPassword.Text;
            }
        }

        private void bLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Password) || string.IsNullOrEmpty(this.UserName))
                return;

            this.DialogResult = DialogResult.OK;

            this.Close();
        }
    }
}
