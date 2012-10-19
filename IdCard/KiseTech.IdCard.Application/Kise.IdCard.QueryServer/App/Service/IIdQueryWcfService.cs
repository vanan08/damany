using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Kise.IdCard.QueryServer.UI.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IIdQueryWcfService" in both code and config file together.
    [ServiceContract]
    public interface IIdQueryWcfService
    {
        [OperationContract]
        string QueryId(string queryString);

        [OperationContract]
        IdQueryResult QueryByIdNumber(string idNumber);
    }

    [DataContract]
    public class IdQueryResult
    {
        [DataMember]
        public int ErrorCode { get; set; }

        [DataMember]
        public IdCardInfo Info { get; set; }
    }

    [DataContract]
    public class IdCardInfo
    {
        [DataMember]
        public string IdNumber { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int? Sex { get; set; }

        [DataMember]
        public int? Minority { get; set; }

        [DataMember]
        public DateTime? BirthDay { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string IssueDepartment { get; set; }

        [DataMember]
        public DateTime? IssueDate { get; set; }

        [DataMember]
        public DateTime? ValidateUntil { get; set; }

        [DataMember]
        public string Icon { get; set; }

        [DataMember]
        public bool IsWanted { get; set; }
    }
}
