using System;

namespace Kise.IdCard.Server
{
    public class QueryResult
    {
        public Exception Error { get; set; }
        public Kise.IdCard.Server.IdSpec[] IdInfos { get; set; }
    }
}
