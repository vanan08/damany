using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
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

        private Progress<ProgressIndicator> _progressReport;
        private System.Drawing.Color _normalBkColor;

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

        public void ShowQueryResult(IList<string> unmatchFields, bool isSuspect)
        {
            Action doAction = () =>
                                  {
                                      var sb = new StringBuilder();
                                      if (unmatchFields.Count > 0)
                                      {
                                          var m = string.Join(",", unmatchFields);
                                          sb.Append(m).Append("与数据库不符");
                                      }
                                      if (isSuspect)
                                      {
                                          sb.Append("，").Append("网上追逃人员");
                                      }

                                      if (sb.Length == 0)
                                      {
                                          sb.Append("正常");
                                      }

                                      if (isSuspect)
                                      {
                                          this.resultLabel.BackColor = Color.Red;
                                      }
                                      else if (unmatchFields.Count > 0)
                                      {
                                          this.resultLabel.BackColor = Color.Orange;
                                      }
                                      else
                                      {
                                          this.resultLabel.BackColor = _normalBkColor;
                                      }

                                      this.resultLabel.Text = sb.ToString();
                                      this.resultLabel.Visible = true;
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


                    resultLabel.Visible = false;

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
            _normalBkColor = resultLabel.BackColor;


            ILink lnk = null;
            IIdCardReader cardReader = null;

            IPAddress ip = null;
            if (IPAddress.TryParse(Properties.Settings.Default.ServerIp, out ip))
            {

                var serverEp = new IPEndPoint(ip, Properties.Settings.Default.ServerPort);
                lnk = new UdpClient(serverEp);
            }
            else
            {
                lnk = new FakeLink();
            }
            

            if (Program.IsDebug)
            {
                cardReader = new FakeIdCardReader();
            }
            else
            {
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
            if (string.IsNullOrEmpty(Properties.Settings.Default.ServerIp))
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
            _progressReport = new System.Progress<ProgressIndicator>();
            _progressReport.ProgressChanged += (s, arg) =>
            {
                statusLabel.Caption = arg.Status;

                if (arg.LongOperation.HasValue)
                {
                    progressBar.Visibility = arg.LongOperation.Value
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
            _idService.QueryIdAsync(_progressReport, Properties.Settings.Default.ServerIp);
        }

        void inpc_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SetColor();
        }


        private void SetColor()
        {
            //var isNormal = _idCardInfo.IdStatus == Kise.IdCard.Model.IdStatus.UnKnown || _idCardInfo.IdStatus == Kise.IdCard.Model.IdStatus.Normal;
        }

    }
}