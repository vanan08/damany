using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RemoteImaging.Forms
{
    public partial class HostConfig : UserControl
    {
        public HostConfig()
        {
            InitializeComponent();
        }


        public string HostName
        {
            get
            {
                return this.hostName.Text;
            }
            set
            {
                this.hostName.Text = value;
            }
        }


        public event EventHandler ApplyClick
        {
            add
            {
                this.applyButton.Click += value;
            }
            remove
            {
                this.applyButton.Click -= value;
            }
        }
    }
}
