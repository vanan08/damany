using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging
{
    public class DateTimeInString
    {
        public string year;
        public string month;
        public string day;
        public string hour;
        public string minute;
        public string second;

        public static DateTimeInString FromDateTime(DateTime dt)
        {
            DateTimeInString dtString = new DateTimeInString();
            dtString.year = dt.Year.ToString("D4");
            dtString.month = dt.Month.ToString("D2");
            dtString.day = dt.Day.ToString("D2");
            dtString.hour = dt.Hour.ToString("D2");
            dtString.minute = dt.Minute.ToString("D2");
            dtString.second = dt.Second.ToString("D2");

            return dtString;
        }


    }
}
