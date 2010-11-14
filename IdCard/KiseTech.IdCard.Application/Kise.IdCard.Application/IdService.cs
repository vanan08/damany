using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Kise.IdCard.Infrastructure.CardReader;
using Kise.IdCard.Messaging;
using Kise.IdCard.Messaging.Link;


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

        public async void Start()
        {
            _queryService.Start();

            while (true)
            {
                var v = await _idCardReader.ReadAsync();

                CurrentIdCard = v.ToModelIdCardInfo();

                _view.CurrentIdCardInfo = CurrentIdCard;

                var reply = await _queryService.QueryAsync("", CurrentIdCard.IdCardNo);
                if (!reply.IsTimedOut)
                {
                    var statusCode = int.Parse(reply.Message);
                    CurrentIdCard.IdStatus = _statusDict[statusCode];
                }
                else
                {
                    CurrentIdCard.IdStatus = IdStatus.UnKnown;
                }

                IdCardList.Add(CurrentIdCard);
            }
        }

    }
}
