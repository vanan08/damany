using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RemoteImaging
{
    public partial class FormConfigCamera : Form
    {
        public FormConfigCamera()
        {
            InitializeComponent();
        }

        public void Navigate(string uriString)
        {
            this.webBrowser1.Navigate(uriString);
        }

       
    }
}
