using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using Kise.IdCard.Application;
using Kise.IdCard.Infrastructure.CardReader;
using Kise.IdCard.Messaging;
using Kise.IdCard.Messaging.Link;
using Kise.IdCard.Model;
using Kise.IdCard.UI;
using System.Threading;

namespace Kise.IdCard.UI
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm, IIdCardView
    {
        private IdService _idService;

        private EventProgress<ProgressIndicator> _progressReport;
        private bool _isQueryingId;

        public bool CanQueryId
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                if (InvokeRequired)
                {
                    Action<bool> ac = c => CanQueryId = c;
                    this.BeginInvoke(ac, value);
                    return;
                }

                databaseQuery.Enabled  = value;

            }
        }

        public MainForm()
        {
            InitializeComponent();

            idCardControl1.MinorityDictionary = FileMinorityDictionary.Instance;

            ILink lnk = null;
            IIdCardReader cardReader = null;

            if (Program.IsDebug)
            {
                lnk = new TcpClientLink();
                cardReader = new FakeIdCardReader();
            }
            else
            {
                lnk = new SmsLink((string)Properties.Settings.Default.smsModemComPort, 9600);
                cardReader = new IdCardReader(1001);
            }

            _idService = new IdService(cardReader, lnk);
            _idService.AttachView(this);

        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.smsModemComPort)
                || string.IsNullOrEmpty(Properties.Settings.Default.smsCenterNo))
            {
                var dlg = new FormSettings();
                dlg.ShowDialog(this);
            }

            if (Properties.Settings.Default.AutoStart)
            {
                CreateTimer();
                UpdateButtonState(true);
            }
        }

        private void CreateTimer()
        {
        }

        public IdCardInfo CurrentIdCardInfo
        {
            get { return idCardControl1.IdCardInfo; }
            set
            {
                if (InvokeRequired)
                {
                    Action<IdCardInfo> ac = v => this.CurrentIdCardInfo = v;
                    this.BeginInvoke(ac, value);
                    return;
                }

                this.idCardControl1.IdCardInfo = value;
            }
        }



        private void buttonQuery_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new FormIdQuery();
            form.ShowDialog(this);
        }


        private void reportButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            var report = new IdReport();
            report.DataSource = xpCollection1;

            report.ShowPreviewDialog();
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void stopButton_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void UpdateButtonState(bool timerIsRunning)
        {
            startButton.Enabled = !timerIsRunning;
            stopButton.Enabled = timerIsRunning;
        }

        private void startButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            _progressReport = new System.Threading.EventProgress<ProgressIndicator>();
            _progressReport.ProgressChanged += (s, arg) =>
            {
                statusLabel.Caption = arg.Value.Status;

                if (arg.Value.LongOperation.HasValue)
                {
                    progressBar.Visibility = arg.Value.LongOperation.Value == true
                              ? BarItemVisibility.Always
                              : BarItemVisibility.Never;

                }
            };

            _idService.Start(_progressReport);
        }

        private void settingsButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            var dlg = new FormSettings();
            dlg.ShowDialog(this);
        }

        private void databaseQuery_ItemClick(object sender, ItemClickEventArgs e)
        {
            _idService.QueryIdAsync(_progressReport, "123456");
        }
    }
}