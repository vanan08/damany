using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdQueryServiceContract
{
    [Serializable]
    public class QueryResult
    {
        public string NormalResult { get; set; }
        public string SuspectResult { get; set; }
    }
}
