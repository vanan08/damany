using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Kise.IdCard.Infrastructure.CardReader;


namespace Kise.IdCard.Application
{
    using Model;
    using Infrastructure;

    public class IdService
    {
        private readonly IIdCardReader _idCardReader;
        private IIdCardView _view;

        public IdCardInfo CurrentIdCard { get; set; }
        public BindingList<IdCardInfo> IdCardList { get; set; }


        public IdService(IIdCardReader idCardReader)
        {
            if (idCardReader == null) throw new ArgumentNullException("idCardReader");
            _idCardReader = idCardReader;
            IdCardList = new BindingList<IdCardInfo>();
        }

        public void AttachView(IIdCardView view)
        {
            if (view == null) throw new ArgumentNullException("view");
            _view = view;
        }

        public async void Start()
        {
            while (true)
            {
                var v = await _idCardReader.ReadAsync();

                CurrentIdCard = v.ToModelIdCardInfo();

                _view.CurrentIdCardInfo = CurrentIdCard;

                IdCardList.Add(CurrentIdCard);
            }

        }

    }
}
