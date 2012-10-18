using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdQueryServiceContract
{
    [Serializable]
    class IdCardInfo
    {
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Minority { get; set; }
        public DateTime BirthDay { get; set; }
        public string Address { get; set; }
        public string IssueDepartment { get; set; }
        public DateTime IssueDate { get; set; }
        public int ValidYears { get; set; }
        public string Icon { get; set; }
    }
}
