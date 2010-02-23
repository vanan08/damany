using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.EPolice.Common
{
    public class LicensePlate
    {
        public LicensePlate(string area, string number, LicensePlateCategory category)
        {
            this.AreaCode = area;
            this.LicenseNumber = number;
            this.Category = category;
        }

        public string AreaCode
        {
            get;
            set;
        }

        public string LicenseNumber
        {
            get;
            set;
        }

        public LicensePlateCategory Category
        {
            get;
            set;
        }
    }
}
