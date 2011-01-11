using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kise.IdCard.Model;

namespace Kise.IdCard.Application
{
    public interface IIdCardView
    {
        event EventHandler ViewShown;

        IdCardInfo CurrentIdCardInfo { get; set; }
        bool CanQueryId { get; set; }
        bool CanStop { set; }
        bool CanStart { set; }

        void ShowQueryResult(IList<string> unmatchFields, bool isSuspect);
    }
}
