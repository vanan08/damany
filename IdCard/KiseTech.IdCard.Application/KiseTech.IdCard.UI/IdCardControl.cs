using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Kise.IdCard.UI
{
    using Model;

    public partial class IdCardControl : DevExpress.XtraEditors.XtraUserControl
    {

        private Timer _timer;

        public string BirthDayFormat { get; set; }

        public IDictionary<int, string> MinorityDictionary { get; set; }

        public bool IsSuspect { get; set; }
        

        public IdCardControl()
        {
            InitializeComponent();

            InitTimer();

            ClearData();
        }

        private void InitTimer()
        {
            var timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += TimerOnTick;
            timer.Enabled = true;
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            var isSuspect = IsSuspect;
            if (isSuspect)
            {
                labelControlSuspectTip.Visible = !labelControlSuspectTip.Visible;
            }
            else
            {
                labelControlSuspectTip.Visible = false;
            }
        }

        private void ClearData()
        {
            this.minority.Text = null;
            this.name.Text = null;
            this.sex.Text = null;
            this.year.Text = null;
            this.month.Text = null;
            this.day.Text = null;
            this.address.Text = null;
            this.idCardNo.Text = null;
            this.IsSuspect = false;
            this.image.Image = null;
        }
    }
}
