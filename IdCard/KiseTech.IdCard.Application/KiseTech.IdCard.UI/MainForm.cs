using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

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

        }


        private IdCardInfo _idCardInfoLeft;
        public IdCardInfo IdCardInfoLeft
        {
            get { return _idCardInfoLeft; }
            set
            {
                if (value != null)
                {
                    _idCardInfoLeft = value;

                    ShowIdCard(idCardControlLeft, value);

                    var inpc = (INotifyPropertyChanged)_idCardInfoLeft;
                    inpc.PropertyChanged += new PropertyChangedEventHandler(inpc_PropertyChanged);

                }
            }
        }

        private IdCardInfo _idCardInfoRight;
        public IdCardInfo IdCardInfoRight
        {
            get { return _idCardInfoRight; }
            set
            {
                if (value != null)
                {
                    _idCardInfoRight = value;

                    ShowIdCard(idCardControlRight, value);

                    var inpc = (INotifyPropertyChanged)_idCardInfoRight;
                    inpc.PropertyChanged += new PropertyChangedEventHandler(inpc_PropertyChanged);
                }
            }
        }

        private static void ShowIdCard(IdCardControl dest, IdCardInfo info)
        {
            dest.name.Text = info.Name;

            if (info.SexCode.HasValue)
            {
                dest.sex.Text = Model.Helper.GetSexName(info.SexCode.Value);
            }

            if (info.MinorityCode.HasValue)
            {
                dest.minority.Text = MinorityDictionary[info.MinorityCode.Value];
            }

            if (info.BornDate.HasValue)
            {
                dest.year.Text = info.BornDate.Value.Year.ToString();
                dest.month.Text = info.BornDate.Value.Month.ToString();
                dest.day.Text = info.BornDate.Value.Day.ToString();
            }
            dest.address.Text = info.Address;
            //this.issuedBy.Text = _idCardInfoLeft.GrantDept;
            //this.expiry.Text = FormatDate(_idCardInfoLeft.ValidateFrom) + " — " + FormatDate(_idCardInfoLeft.ValidateUntil);
            dest.idCardNo.Text = info.IdCardNo;


            dest.image.Image = info.PhotoData != null ? Image.FromStream(new System.IO.MemoryStream(info.PhotoData)) : null;
            dest.IsSuspect = info.IsSuspect;
        }

        public event EventHandler ViewShown;

        public virtual void OnViewShown(object sender, EventArgs e)
        {
            EventHandler handler = ViewShown;
            if (handler != null)
                handler(sender, e);
        }


        public static IDictionary<int, string> MinorityDictionary { get; set; }

        public MainForm()
        {
            InitializeComponent();

            MinorityDictionary = FileMinorityDictionary.Instance;

            IIdCardReader cardReader = null;

            if (Program.IsDebug)
            {
                cardReader = new FakeIdCardReader();
            }
            else
            {
                cardReader = new IdCardReader(1001);
            }

            _idService = new IdService(cardReader);
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
            get { return IdCardInfoLeft ; }
            set
            {
                if (InvokeRequired)
                {
                    Action<IdCardInfo> ac = v => this.CurrentIdCardInfo = v;
                    this.BeginInvoke(ac, value);
                    return;
                }

                this.IdCardInfoLeft = value;
                
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

        private async void databaseQuery_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentIdCardInfo == null)
            {
                MessageBox.Show("请先读取身份证，或者手动输入号码查询");
                return;
            }

            await QueryIdCardFromUi(CurrentIdCardInfo.IdCardNo);
        }

        private async Task QueryIdCardFromUi(string idNo)
        {
            databaseQuery.Enabled = false;
            buttonEditManualQuery.Enabled = false;

            if (string.IsNullOrEmpty(idNo))
            {
                MessageBox.Show("请输入有效身份证号码");
                return;
            }

            if (idNo.Length != 15 && idNo.Length != 18)
            {
                MessageBox.Show("身份证长度不足15位或者18位");
                return;
            }


            try
            {
                var queryClient = new QueryService();
                var result = await queryClient.QueryByIdNumberAsync(idNo);


                if (result.ErrorCode == 0)
                {
                    if (result.Info != null)
                    {
                        var msg =
                            string.Format(
                                "name: {0}, sex:{1}, minority:{2}, birthday:{3}, add:{4}, issueDate:{5}, idNumber:{6}",
                                result.Info.Name, result.Info.Sex, result.Info.Minority, result.Info.BirthDay,
                                result.Info.Address, result.Info.IssueDate, result.Info.IdNumber);

                        _logger.Trace(msg);

                        var info = ConvertWcfToInfo(result.Info);
                        IdCardInfoRight = info;
                    }
                    else
                    {
                        MessageBox.Show("没有查询到身份证信息，请确保身份证号码输入正确");
                    }
                }
                else
                {
                    MessageBox.Show("查询身份证出现错误，请稍后重试");
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("查询身份证出现异常，请重试");
            }
            finally
            {
                databaseQuery.Enabled = true;
                buttonEditManualQuery.Enabled = true;
            }
           
        }

        void inpc_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SetColor();
        }


        private void SetColor()
        {
            //var isNormal = _idCardInfoLeft.IdStatus == Kise.IdCard.Model.IdStatus.UnKnown || _idCardInfoLeft.IdStatus == Kise.IdCard.Model.IdStatus.Normal;
        }

        private void barCheckItemManulQuery_DownChanged(object sender, ItemClickEventArgs e)
        {
        }

        private async void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            await QueryIdCardFromUi((string) buttonEditManualQuery.EditValue);
        }

       

        private IdCardInfo ConvertWcfToInfo(Messaging.WcfService.IdCardInfo info)
        {
            var result = new IdCardInfo();
            result.Address = info.Address;
            result.BornDate = info.BirthDay;
            result.GrantDept = info.IssueDepartment;
            result.IdCardNo = info.IdNumber;
            result.IsSuspect = info.IsWanted;
            result.MinorityCode = info.Minority;
            result.Name = info.Name;
            result.SexCode = info.Sex;
            result.ValidateFrom = info.IssueDate;

            if (!string.IsNullOrEmpty(info.Icon))
            {
                result.PhotoData = Convert.FromBase64String(info.Icon);
            }

            return result;
        }

        private void buttonEditManualQuery_Enter(object sender, EventArgs e)
        {
            buttonEditManualQuery.SelectAll();
        }

        private void buttonEditManualQuery_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private async void buttonEditManualQuery_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await QueryIdCardFromUi((string)buttonEditManualQuery.EditValue);
            }
        }

        private void buttonEditManualQuery_MouseUp(object sender, MouseEventArgs e)
        {
           // buttonEditManualQuery.SelectAll();

        }

        private void buttonEditManualQuery_MouseEnter(object sender, EventArgs e)
        {
            //buttonEditManualQuery.SelectAll();
        }
    }
} 