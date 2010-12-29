using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kise.IdCard.Server
{
    public interface IIdQueryService
    {
        string QueryIdCard(string queryType, string queryString);
    }
}
