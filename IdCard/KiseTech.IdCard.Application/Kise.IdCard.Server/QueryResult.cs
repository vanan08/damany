using System;

namespace Kise.IdCard.Server
{
    public class QueryResult
    {
        public Exception Error { get; set; }
        public IdCard.Model.IdCardInfo[] IdInfos { get; set; }
    }
}
