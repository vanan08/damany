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

        private IDictionary<int, IdStatus> _statusDict;

        public IdCardInfo CurrentIdCard { get; set; }
        public BindingList<IdCardInfo> IdCardList { get; set; }

        private readonly object _isBusyLock = new object();
        private bool _isBusy;
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

        public async Task QueryIdAsync(IProgress<ProgressIndicator> progressReport, string destinationNo)
        {
            IsBusy = true;
            _queryService.Start();
            var indicator = new ProgressIndicator();
            IdInfo v = null;
            try
            {
                await TaskEx.Delay(3000);
                indicator.Status = "读取身份证...";
                progressReport.Report(indicator);
                await TaskEx.Delay(1000);
                v = await _idCardReader.ReadAsync();
            }
            catch (Exception)
            {
                indicator.Status = "身份证读取失败!";
                progressReport.Report(indicator);
                return;
            }

            indicator.Status = "身份证读取成功";
            progressReport.Report(indicator);

            CurrentIdCard = v.ToModelIdCardInfo();
            _view.CurrentIdCardInfo = CurrentIdCard;

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
                CurrentIdCard.IdStatus = _statusDict[statusCode];
            }
            else
            {
                CurrentIdCard.IdStatus = IdStatus.UnKnown;
                await TaskEx.Delay(3000);
            }

            IdCardList.Add(CurrentIdCard);
            CurrentIdCard.Save();
        }

    }
}
