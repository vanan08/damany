using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Police.DTO
{
    public class Vehicle
    {
        public string Category
        {
            get;
            set;
        }

        public int VehicleID
        {
            get;
            set;
        }

        public string LicenseNumber
        {
            get;
            set;
        }

        public string LicenseAreaCode
        {
            get;
            set;
        }

        public TrafficLawViolationEvent TrafficLawViolationEvent
        {
            get;
            set;
        }

        public int OwnerID
        {
            get;
            set;
        }
    }
}
