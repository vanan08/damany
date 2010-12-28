using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace IdQueryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "IdQueryProvider" in both code and config file together.
    public class IdQueryProvider : IIdQueryProvider
    {
        public string QueryIdCard(string queryType, string queryString)
        {
            throw new NotImplementedException();
        }
    }
}
