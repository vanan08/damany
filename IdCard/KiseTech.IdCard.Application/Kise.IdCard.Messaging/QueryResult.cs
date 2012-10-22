using System;

namespace Kise.IdCard.Messaging
{
    public class QueryResult
    {
        public int ErrorCode; //0: no error occurred, 1: Query String error, 2: server side error
        public bool IsSuspect;
        public Model.IdCardInfo IdInfo;

        public QueryResult()
        {
            ErrorCode = 0;
            IsSuspect = false;
        }
    }
}
