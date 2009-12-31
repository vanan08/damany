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
                return this.tbUsername.Text;
            }
        }

        public string PassWord
        {
            get
            {
                return this.tbPassword.Text;
            }
        }
    }
}
