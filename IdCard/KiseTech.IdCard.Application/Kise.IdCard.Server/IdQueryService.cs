using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kise.IdCard.Server
{
    public class IdQueryService
    {
        RBSPAdapter_COM.RSBPAdapterCOMObjClass _queryProvider;

        public string QueryAsync(string queryString)
        {
            if (_queryProvider == null)
            {
                _queryProvider = new RBSPAdapter_COM.RSBPAdapterCOMObjClass();
            }

            _queryProvider.queryCondition = queryString;
            _queryProvider.queryType = "QueryQGRK";

            return _queryProvider.queryCondition;

        }
    }
}
