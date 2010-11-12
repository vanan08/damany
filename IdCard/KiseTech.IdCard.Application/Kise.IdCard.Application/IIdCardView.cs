using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kise.IdCard.Model;

namespace Kise.IdCard.Application
{
    public interface IIdCardView
    {
        IdCardInfo CurrentIdCardInfo { get; set; }
    }
}
