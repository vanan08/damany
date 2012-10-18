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
        IdCardInfo QueryByIdNumber(string idNumber);
    }


    [DataContract]
    public class IdCardInfo
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Sex { get; set; }

        [DataMember]
        public string Minority { get; set; }

        [DataMember]
        public DateTime BirthDay { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string IssueDepartment { get; set; }

        [DataMember]
        public DateTime IssueDate { get; set; }

        [DataMember]
        public int ValidYears { get; set; }

        [DataMember]
        public string Icon { get; set; }
    }
}
