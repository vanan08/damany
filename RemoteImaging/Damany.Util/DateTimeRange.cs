using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Util
{
    public class DateTimeRange
    {
        public DateTimeRange ( DateTime from, DateTime to )
        {
            if (from > to)
            {
                throw new ArgumentException("from is bigger than to");
            }


            this.From = from;
            this.To = to;
        }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
