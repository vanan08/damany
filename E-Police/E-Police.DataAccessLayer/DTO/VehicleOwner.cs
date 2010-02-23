using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Police.DTO
{
    public class VehicleOwner
    {
        public string Name
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public string PhoneNumber
        {
            get;
            set;
        }

        public int OwnerID
        {
            get;
            set;
        }

        public Vehicle Vehicle
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
