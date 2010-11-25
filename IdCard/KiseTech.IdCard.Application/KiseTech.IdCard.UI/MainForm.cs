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

namespace Kise.IdCard.UI
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm, IIdCardView
    {
        private IdService _idService;
        private Timer _queryTimer;

        private bool _isQueryingId;

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
                _queryTimer.Enabled = true;
                UpdateButtonState(true);
            }
        }

        private void CreateTimer()
        {
            _queryTimer = new Timer() { Interval = 3000 };
            _queryTimer.Tick += timer1_Tick;
        }

        public IdCardInfo CurrentIdCardInfo
        {
            get { return idCardControl1.IdCardInfo; }
            set { idCardControl1.IdCardInfo = value; }
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
            if (_isQueryingId) return;

            _isQueryingId = true;

            var progress = new System.Threading.EventProgress<ProgressIndicator>();
            progress.ProgressChanged += (s, arg) =>
                                            {
                                                statusLabel.Caption = arg.Value.Status;
                                                progressBar.Visibility = arg.Value.LongOperation
                                                                             ? BarItemVisibility.Always
                                                                             : BarItemVisibility.Never;
                                            };
            await _idService.QueryIdAsync(progress, (string)Properties.Settings.Default.smsCenterNo);
            _isQueryingId = false;

        }

        private void stopButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_queryTimer != null)
            {
                _queryTimer.Stop();

                UpdateButtonState(false);
            }
        }

        private void UpdateButtonState(bool timerIsRunning)
        {
            startButton.Enabled = !timerIsRunning;
            stopButton.Enabled = timerIsRunning;
        }

        private void startButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_queryTimer == null)
            {
                CreateTimer();
            }

            if (_queryTimer != null)
            {
                _queryTimer.Start();
                UpdateButtonState(true);
            }
        }

        private void settingsButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            var dlg = new FormSettings();
            dlg.ShowDialog(this);
        }
    }
}