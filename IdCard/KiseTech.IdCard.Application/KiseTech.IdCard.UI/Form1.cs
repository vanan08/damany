using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kise.IdCard.Infrastructure.Sms;

namespace KiseTech.IdCard.UI
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        //ISmsService sms = new SmsService("com3", 9600);

        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
