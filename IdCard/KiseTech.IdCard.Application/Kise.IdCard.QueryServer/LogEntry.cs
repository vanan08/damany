using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kise.IdCard.Server
{
    public class LogEntry
    {
        public DateTime Time { get; set; }
        public string Sender { get; set; }
        public string Description { get; set; }

        public LogEntry()
        {
            Time = DateTime.Now;
        }
    }
}
