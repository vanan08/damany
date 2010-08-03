using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Police.DTO
{
    public class TrafficMonitorSpot
    {
        public int MonitorSpotID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int MonitoredBy
        {
            get;
            set;
        }

        public string ConnectionString
        {
            get;
            set;
        }

        public TrafficLawViolationEvent TrafficLawViolationEvent
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
