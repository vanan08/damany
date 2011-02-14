using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDetectorTester.Models
{
    public class ResponseData
    {
        public ResponseData()
        {
            Time = DateTime.Now;
        }

        public string ChannelId { get; set; }
        public string Data1 { get; set; }
        public string Data2 { get; set; }
        public string Data3 { get; set; }
        public DateTime? Time { get; set; }

    }
}
