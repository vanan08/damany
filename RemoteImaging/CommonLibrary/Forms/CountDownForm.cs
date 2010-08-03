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
    public partial class CountDownForm : Form
    {
        public int SecondsToCount { get; set; }


        public string Message
        {
            get
            {
                return message.Text;
            }
            set
            {
                message.Text = value;
            }
        }

        public CountDownForm()
        {
            InitializeComponent();


        }

        private void CountDownForm_Load(object sender, EventArgs e)
        {
            var timer = new Timer();
            timer.Interval = Interval;

            timer.Tick += timer_Tick;
            timer.Enabled = true;

            UpdateButtonText();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            SecondsToCount -= 1;

            UpdateButtonText();

            if (SecondsToCount <= 0)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void UpdateButtonText()
        {
            var text = string.Format(Format, SecondsToCount);
            buttonOk.Text = text;
        }

        private const int Interval = 1000;
        private const string Format = "({0})确定";

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
