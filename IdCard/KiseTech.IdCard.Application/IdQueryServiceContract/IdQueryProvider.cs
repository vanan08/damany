using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdQueryServiceContract
{
    public interface IIdQueryProvider
    {
        QueryResult QueryIdCard(string queryString);
    }
}
