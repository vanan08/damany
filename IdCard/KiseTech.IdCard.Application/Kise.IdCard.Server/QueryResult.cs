using System;

namespace Kise.IdCard.Server
{
    public class QueryResult
    {
        public Exception Error { get; set; }
        public Kise.IdCard.Server.IdSpec IdInfo { get; set; }

        public QueryResult()
        {
            IdInfo = new Kise.IdCard.Server.IdSpec();
        }
    }
}
