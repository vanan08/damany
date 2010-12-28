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
            RBSPAdapter_COM.RSBPAdapterCOMObjClass q = null;
            try
            {
                Console.WriteLine(queryString + queryType);
                q = new RBSPAdapter_COM.RSBPAdapterCOMObjClass();
                q.queryCondition = queryString;
                q.queryType = queryType;
                var result = q.queryCondition;
                Console.WriteLine(result);
                return result;
            }
            finally
            {
                if (q != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(q);
                }
            }
        }
    }
}
