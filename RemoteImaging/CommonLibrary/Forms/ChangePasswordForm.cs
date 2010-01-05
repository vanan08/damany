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
    public partial class ChangePasswordForm : Form
    {
        public ChangePasswordForm()
        {
            InitializeComponent();
        }

        public string UserName
        {
            get
            {
                return this.tbUsername.Text;
            }
            set
            {
                this.tbUsername.Text = value;
            }
        }

        public string OldPassword
        {
            get
            {
                return this.oldPassword.Text;
            }
            set
            {
                this.oldPassword.Text = value;
            }
        }

        public string NewPassword
        {
            get
            {
                return this.newPassword.Text;
            }
            set
            {
                this.newPassword.Text = value;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = false;
            base.OnFormClosing(e);
        }

        private void repeatedPassword_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = string.Compare(this.repeatedPassword.Text,
                                        this.newPassword.Text,
                                        false
                                        ) != 0;

            this.errorProvider.SetError(this.repeatedPassword,
                e.Cancel ? "两次输入的密码不一致。" : string.Empty);
        }
    }
}
