using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kise.IdCard.Infrastructure.CardReader;
using Kise.IdCard.Messaging;
using Kise.IdCard.Messaging.Link;
using System.Threading;


namespace Kise.IdCard.Application
{
    using Model;
    using Infrastructure;

    public class IdService
    {
        private readonly IIdCardReader _idCardReader;
        private readonly ILink _link;
        private QueryService _queryService;
        private IIdCardView _view;

        private System.Timers.Timer _timer;
        private IDictionary<int, IdStatus> _statusDict;

        public IdCardInfo CurrentIdCard { get; set; }
        public BindingList<IdCardInfo> IdCardList { get; set; }

        private readonly object _isBusyLock = new object();
        private bool _isBusy;
        private IProgress<ProgressIndicator> _progressReport;

        private bool _isQuerying;
        private bool _isReadingCard;

        private object _statusLock = new object();

        public bool IsBusy
        {
            get
            {
                lock (_isBusyLock)
                {
                    return _isBusy;
                }
            }

            private set
            {
                lock (_isBusyLock)
                {
                    _isBusy = value;
                }
            }
        }


        public IdService(IIdCardReader idCardReader, ILink link)
        {
            if (idCardReader == null) throw new ArgumentNullException("idCardReader");
            if (link == null) throw new ArgumentNullException("link");
            _idCardReader = idCardReader;
            _link = link;
            _queryService = new QueryService(_link);
            IdCardList = new BindingList<IdCardInfo>();
            CurrentState = Status.Idle;

            CurrentIdCard = new IdCardInfo();
            CurrentIdCard.IdCardNo = string.Empty;

            _statusDict = new Dictionary<int, IdStatus>();
            _statusDict.Add(Messaging.IdStatus.Normal, IdStatus.Normal);
            _statusDict.Add(Messaging.IdStatus.Killer, IdStatus.WasLawBreaker);
            _statusDict.Add(Messaging.IdStatus.PrisonBreaker, IdStatus.PrisonBreaker);
            _statusDict.Add(Messaging.IdStatus.Wanted, IdStatus.Wanted);
            _statusDict.Add(Messaging.IdStatus.Undefined, IdStatus.UnKnown);
        }

        public void AttachView(IIdCardView view)
        {
            if (view == null) throw new ArgumentNullException("view");
            _view = view;
        }

        public void Start(IProgress<ProgressIndicator> progressReport)
        {
            _progressReport = progressReport;
            _timer = new System.Timers.Timer();
            _timer.Interval = 3000;
            _timer.AutoReset = false;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);

            _timer.Enabled = true;
            _view.CanStop = true;
            _view.CanStart = false;

            TaskEx.Run(() =>
                           {
                               try
                               {
                                   _link.Start();
                               }
                               catch (Exception e)
                               {
                                   MessageBox.Show(e.Message);
                               }

                           });
        }

        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Dispose();
            }

            _view.CanStart = true;
            _view.CanStop = false;
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            try
            {
                //if (CurrentState == Status.QueryingIdCard) return;
                _timer.Stop();
                if (_isQuerying) return;

                CurrentState = Status.ReadingIdCard;
                //_view.CanQueryId = false;

                var idinfo = ReadIdCard(_progressReport);
                var newId = idinfo.Result;
                if (_isQuerying || newId.IdCardNo == CurrentIdCard.IdCardNo) return;

                CurrentIdCard = newId;
                _view.CurrentIdCardInfo = CurrentIdCard;

                if (!_isQuerying)
                {
                    _view.CanQueryId = true;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                _timer.Start();
                //_view.CanQueryId = true;
                CurrentState = Status.Idle;
            }
        }


        public async void QueryIdAsync(IProgress<ProgressIndicator> progressReport, string destinationNo)
        {
            //if (CurrentState == Status.ReadingIdCard) return;

            CurrentState = Status.QueryingIdCard;
            _view.CanQueryId = false;

            IsBusy = true;
            _isQuerying = true;
            bool shouldReturn;

            var indicator = new ProgressIndicator();

            indicator.Status = "查询身份证，请稍候...";
            indicator.LongOperation = true;
            progressReport.Report(indicator);

            var reply = await _queryService.QueryAsync(destinationNo, CurrentIdCard.IdCardNo);
            IsBusy = false;

            indicator.Status = reply.IsTimedOut ? "查询身份证失败（超时）!" : "查询身份证成功";
            indicator.LongOperation = false;
            progressReport.Report(indicator);

            if (!reply.IsTimedOut)
            {
                var splits = reply.Message.Split(new[] { '*' });
                //消息应答格式 id*errorCode*Name*SexCode*minorityCode*birthDay*isSuspect
                if (splits.Length != 7 && splits.Length != 2)
                {
                    MessageBox.Show("收到格式错误的应答，请联系技术人员或者稍侯重试。");
                }
                else
                {
                    var idNo = splits[0];
                    var errorCode = int.Parse(splits[1]);
                    if (errorCode == 1 || splits.Length == 2)
                    {
                        MessageBox.Show("后端服务器查询错误，请联系技术人员或者稍侯重试。");
                    }
                    else
                    {
                        var s = splits[2];
                        string name = string.Empty;
                        if (s != Constants.EmptyString)
                        {
                            name = s;
                        }

                        s = splits[3];
                        int? sexCode = null;
                        if (s != Constants.EmptyString)
                        {
                            sexCode = int.Parse(s);
                        }

                        s = splits[4];
                        int? minorityCode = null;
                        if (s != Constants.EmptyString)
                        {
                            minorityCode = int.Parse(s);
                        }

                        s = splits[5];
                        DateTime? birthDay = null;
                        if (s != Constants.EmptyString)
                        {
                            birthDay = Util.Helper.ParseDatetime(splits[5]);
                        }

                        var isSuspect = splits[6] == "1";

                        var unmatches = new List<string>();
                        if (!string.IsNullOrEmpty(CurrentIdCard.Name) && !string.IsNullOrEmpty(name))
                        {
                            if (CurrentIdCard.Name != name)
                            {
                                unmatches.Add("姓名");
                            }
                        }

                        if (CurrentIdCard.SexCode.HasValue && sexCode.HasValue)
                        {
                            if (CurrentIdCard.SexCode.Value != sexCode.Value)
                            {
                                unmatches.Add("性别");
                            }
                        }

                        if (CurrentIdCard.MinorityCode.HasValue && minorityCode.HasValue)
                        {
                            if (CurrentIdCard.MinorityCode.Value != minorityCode.Value)
                            {
                                unmatches.Add("民族");
                            }
                        }

                        if (CurrentIdCard.BornDate.HasValue && birthDay.HasValue)
                        {
                            if (CurrentIdCard.BornDate.Value != birthDay.Value)
                            {
                                unmatches.Add("出生日期");
                            }
                        }

                        _view.ShowQueryResult(unmatches, isSuspect);

                    }

                }
            }
            else
            {
                MessageBox.Show("服务器没有响应。请联系技术人员或者稍侯重试。");
            }

            IdCardList.Add(CurrentIdCard);
            CurrentIdCard.Save();

            CurrentState = Status.Idle;
            _view.CanQueryId = true;
            _isQuerying = false;
            //_timer.Enabled = true;
        }

        private string PackMessage(IdCardInfo idCard)
        {
            var ss = new string[] { idCard.IdCardNo, idCard.Name, idCard.SexCode.ToString() };
            var returnString = string.Join("*", ss);
            return returnString;
        }

        private async Task<IdCardInfo> ReadIdCard(IProgress<ProgressIndicator> progressReport)
        {
            _queryService.Start();
            var indicator = new ProgressIndicator();
            IdCardInfo v = null;
            try
            {
                indicator.Status = "读取身份证...";
                progressReport.Report(indicator);
                v = await _idCardReader.ReadAsync();
            }
            catch (Exception)
            {
                indicator.Status = "身份证读取失败!";
                progressReport.Report(indicator);
                throw;
            }

            indicator.Status = "身份证读取成功";
            progressReport.Report(indicator);

            return v;
        }

        private Status _currentState;
        private Status CurrentState
        {
            get
            {
                lock (_statusLock)
                {
                    return _currentState;
                }
            }
            set
            {
                lock (_statusLock)
                {
                    _currentState = value;
                }
            }
        }

    }

}