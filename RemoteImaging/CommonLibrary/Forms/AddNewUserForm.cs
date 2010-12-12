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
    public partial class AddNewUserForm : Form
    {
        public AddNewUserForm()
        {
            InitializeComponent();
        }


        public String UserName
        {
            get
            {
                return this.userName.Text;
            }
        }

        public string PassWord
        {
            get
            {
                return this.passWord.Text;
            }
        }

        private void repeatPassword_Validating(object sender, CancelEventArgs e)
        {
            bool valid = string.Compare(
                                this.repeatPassword.Text,
                                this.passWord.Text,
                                false) == 0;

            if (!valid)
            {
                e.Cancel = true;
                this.errorProvider.SetError(this.repeatPassword, "两次输入的密码不一致。");
            }
            else
            {
                this.errorProvider.SetError(this.repeatPassword, string.Empty);
            }
        }
    }
}
