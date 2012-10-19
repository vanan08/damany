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


        public IdService(IIdCardReader idCardReader)
        {
            if (idCardReader == null) throw new ArgumentNullException("idCardReader");
            _idCardReader = idCardReader;
            _queryService = new QueryService();
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
        }

        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Stop();
            }

            _view.CanStart = true;
            _view.CanStop = false;
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            try
            {
                //if (CurrentState == Status.QueryingIdCard) return;
                _timer.Enabled = false;
                if (_isQuerying) return;

                CurrentState = Status.ReadingIdCard;
                //_view.CanQueryId = false;

                var idinfo = ReadIdCard(_progressReport);
                var newId = idinfo.Result;
                if (_isQuerying || newId.IdCardNo == CurrentIdCard.IdCardNo) return;

                CurrentIdCard = newId;
                _view.CurrentIdCardInfo = CurrentIdCard;
            }
            finally
            {
                _timer.Enabled = true;
                //_view.CanQueryId = true;
                CurrentState = Status.Idle;
            }
        }

        private string PackMessage(IdCardInfo idCard)
        {
            var ss = new string[] { idCard.IdCardNo, idCard.Name, idCard.SexCode.ToString() };
            var returnString = string.Join("*", ss);
            return returnString;
        }

        private async Task<IdCardInfo> ReadIdCard(IProgress<ProgressIndicator> progressReport)
        {
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