using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace IdQueryService
{
    [DataContract]
    public class IdQueryResult
    {
        [DataMember]
        public string NormalResult { get; set; }

        [DataMember]
        public string AtLargeResult { get; set; }
    }
}
