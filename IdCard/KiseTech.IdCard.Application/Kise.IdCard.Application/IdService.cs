﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kise.IdCard.Application
{
    using Model;
    using Infrastructure;

    public class IdService
    {
        public IdcardInfo ReadCard()
        {
            throw new NotImplementedException();
        }

        public void Query(IdcardInfo cardToQuery,
            Action<bool> deliveryCallback, Action<IdQueryResponse> responseCallBack)
        {
            throw new NotImplementedException();
        }

    }
}
