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
        private System.Drawing.Color _originalBkColor;

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

                databaseQuery.Enabled = value;
            }
        }

        public bool CanStop
        {
            set
            {
                if (InvokeRequired)
                {
                    Action<bool> ac = c => CanQueryId = c;
                    this.BeginInvoke(ac, value);
                    return;
                }

                stopButton.Enabled = value;
            }
        }

        public bool CanStart
        {
            set
            {
                if (InvokeRequired)
                {
                    Action<bool> ac = c => CanQueryId = c;
                    this.BeginInvoke(ac, value);
                    return;
                }

                startButton.Enabled = value;
            }
        }

        public void ShowQueryResult(Image image, string unmatchResult, bool isSuspect)
        {
            Action doAction = () =>
                                  {
                                      var form = new FormQueryResult(image, unmatchResult, isSuspect);
                                      form.ShowDialog(this);
                                  };
            this.BeginInvoke(doAction);
        }


        private IdCardInfo _idCardInfo;
        public IdCardInfo IdCardInfo
        {
            get { return _idCardInfo; }
            set
            {
                if (value != null)
                {
                    _idCardInfo = value;

                    this.name.Text = _idCardInfo.Name;

                    if (_idCardInfo.SexCode.HasValue)
                    {
                        this.sex.Text = Model.Helper.GetSexName(_idCardInfo.SexCode.Value);
                    }

                    if (_idCardInfo.MinorityCode.HasValue)
                    {
                        this.minority.Text = MinorityDictionary[_idCardInfo.MinorityCode.Value];
                    }

                    if (_idCardInfo.BornDate.HasValue)
                    {
                        this.year.Text = _idCardInfo.BornDate.Value.Year.ToString();
                        this.month.Text = _idCardInfo.BornDate.Value.Month.ToString();
                        this.day.Text = _idCardInfo.BornDate.Value.Day.ToString();
                    }
                    this.address.Text = _idCardInfo.Address;
                    //this.issuedBy.Text = _idCardInfo.GrantDept;
                    //this.expiry.Text = FormatDate(_idCardInfo.ValidateFrom) + " — " + FormatDate(_idCardInfo.ValidateUntil);
                    this.idCardNo.Text = _idCardInfo.IdCardNo;
                    this.idCardStatus.Text = _idCardInfo.QueryResult;

                    SetColor();

                    this.image.Image = Image.FromStream(new System.IO.MemoryStream(_idCardInfo.PhotoData));

                    var inpc = (INotifyPropertyChanged)_idCardInfo;
                    inpc.PropertyChanged += new PropertyChangedEventHandler(inpc_PropertyChanged);

                }

            }
        }

        public event EventHandler ViewShown;

        public virtual void OnViewShown(object sender, EventArgs e)
        {
            EventHandler handler = ViewShown;
            if (handler != null)
                handler(sender, e);
        }


        public IDictionary<int, string> MinorityDictionary { get; set; }

        public MainForm()
        {
            InitializeComponent();

            MinorityDictionary = FileMinorityDictionary.Instance;
            _originalBkColor = idCardStatus.BackColor;


            ILink lnk = null;
            IIdCardReader cardReader = null;

            if (Program.IsDebug)
            {
                lnk = new TcpClientLink();
                (lnk as TcpClientLink).PortToConnect = 10000;

                cardReader = new FakeIdCardReader();
            }
            else
            {
                lnk = new SmsLink((string)Properties.Settings.Default.smsModemComPort, 9600);
                cardReader = new IdCardReader(1001);
            }

            _idService = new IdService(cardReader, lnk);
            _idService.AttachView(this);

            this.Shown += (s, e) =>
                {
                    if (Properties.Settings.Default.AutoStart)
                    {
                        this.startButton_ItemClick(null, null);
                    }
                };

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
            get { throw new NotImplementedException(); }
            set
            {
                if (InvokeRequired)
                {
                    Action<IdCardInfo> ac = v => this.CurrentIdCardInfo = v;
                    this.BeginInvoke(ac, value);
                    return;
                }

                this.IdCardInfo = value;
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
            _idService.Stop();
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
            _idService.QueryIdAsync(_progressReport, Properties.Settings.Default.smsCenterNo);
        }

        void inpc_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            idCardStatus.Text = _idCardInfo.QueryResult;
            SetColor();
        }


        private void SetColor()
        {
            //var isNormal = _idCardInfo.IdStatus == Kise.IdCard.Model.IdStatus.UnKnown || _idCardInfo.IdStatus == Kise.IdCard.Model.IdStatus.Normal;
            this.idCardStatus.BackColor = _idCardInfo.IsSuspect ? Color.Red : _originalBkColor;
        }

    }
}