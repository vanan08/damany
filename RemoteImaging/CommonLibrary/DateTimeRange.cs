using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.RemoteImaging.Common
{
    public class DateTimeRange
    {
        public DateTimeRange(DateTime begin, DateTime end)
        {
            if (begin > end)
                throw new System.InvalidOperationException("begin time should be less than end time");

            this._Begin = begin;
            this._End = end;
        }

        private DateTime _Begin;
        public DateTime Begin
        {
            get
            {
                return _Begin;
            }

        }


        private DateTime _End;
        public DateTime End
        {
            get
            {
                return _End;
            }
        }
    }
}
