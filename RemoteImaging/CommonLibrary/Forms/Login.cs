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
        public event EventHandler LabelClicked
        {
            add
            {
                this.linkLabel1.Click += value;
            }
            remove
            {
                this.linkLabel1.Click -= value;

            }
        }

        public event EventHandler LoginButtonClick
        {
            add
            {
                this.bLogin.Click += value;
            }
            remove
            {
                this.bLogin.Click -= value;
            }
        }

        public event EventHandler CancelButtonClick
        {
            add
            {
                this.cancelButton.Click += value;

            }
            remove
            {
                this.cancelButton.Click -= value;
            }
        }

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
    }
}
