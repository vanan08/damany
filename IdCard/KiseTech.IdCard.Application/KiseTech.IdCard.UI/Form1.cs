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
    public partial class Form1 : Form
    {
        ISmsService sms = new SmsService("com3", 9600);

        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            response.Text = "";
            button1.Enabled = false;

            var res = await sms.QueryAsync(destinationNo.Text, msgToSend.Text);
            response.Text = res;

            button1.Enabled = true;
        }
    }
}
