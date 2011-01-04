using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading;
using IdQueryServiceContract;
using RBSPAdapter_COM;

namespace IdQueryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "IdQueryProvider" in both code and config file together.
    public class IdQueryProvider : MarshalByRefObject, IdQueryServiceContract.IIdQueryProvider
    {
        public QueryResult QueryIdCard(string queryString)
        {
            Console.WriteLine("receive query: queryString = " + queryString + "\r\n");
            var q = new RBSPAdapter_COM.RSBPAdapterCOMObjClass();

            q.queryCondition = queryString;
            q.queryType = "QueryQGRK";
            var normalResult = q.queryCondition;
            Console.WriteLine(normalResult);

            q.queryType = "QueryZTK";
            var suspectResult = string.Empty;
            //suspectResult = q.queryCondition;
            //Console.WriteLine(suspectResult);

            Console.WriteLine("---------------");

            var result = new QueryResult() { NormalResult = normalResult, SuspectResult = suspectResult };
            System.Runtime.InteropServices.Marshal.ReleaseComObject(q);

            return result;
        }
    }




}
