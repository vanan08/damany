using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _timer.AutoReset = true;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);

            _timer.Enabled = true;
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (CurrentState == Status.QueryingIdCard) return;

            CurrentState = Status.ReadingIdCard;
            _view.CanQueryId = false;

            try
            {
                var idinfo = ReadIdCard(_progressReport);
                if (CurrentState != Status.QueryingIdCard)
                {
                    CurrentIdCard = idinfo.Result;
                    _view.CurrentIdCardInfo = CurrentIdCard;

                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                _timer.Enabled = true;
                _view.CanQueryId = true;
                CurrentState = Status.Idle;
            }
        }


        public async Task QueryIdAsync(IProgress<ProgressIndicator> progressReport, string destinationNo)
        {
            if (CurrentState == Status.ReadingIdCard) return;

            CurrentState = Status.QueryingIdCard;
            _view.CanQueryId = false;

            IsBusy = true;
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
                var statusCode = int.Parse(reply.Message);
                if (_statusDict.ContainsKey(statusCode))
                {
                    CurrentIdCard.IdStatus = _statusDict[statusCode];
                }
                else
                {
                    CurrentIdCard.IdStatus = IdStatus.UnKnown;
                }

            }
            else
            {
                CurrentIdCard.IdStatus = IdStatus.UnKnown;
                await TaskEx.Delay(3000);
            }

            IdCardList.Add(CurrentIdCard);
            CurrentIdCard.Save();

            CurrentState = Status.Idle;
            _view.CanQueryId = true;
            //_timer.Enabled = true;
        }

        private async Task<IdCardInfo> ReadIdCard(IProgress<ProgressIndicator> progressReport)
        {
            _queryService.Start();
            var indicator = new ProgressIndicator();
            IdInfo v = null;
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

            return v.ToModelIdCardInfo();
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