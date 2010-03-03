using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Police.DTO
{
    public class TrafficLawViolationEvent
    {

        public System.DateTime Time
        {
            get;
            set;
        }

        public System.Drawing.Image EvidencePicture
        {
            get;
            set;
        }

        /// <summary>
        /// 违法行为:   闯红灯等等
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        public int? VehicleID
        {
            get;
            set;
        }

        public int? CapturedAt
        {
            get;
            set;
        }

        public int EventID
        {
            get;
            set;
        }
    }
}
