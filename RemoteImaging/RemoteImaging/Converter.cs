using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Contracts;

namespace RemoteImaging
{
    public static class Converter
    {
        public static string ToFileName(this DateTime dt)
        {
            string name = string.Format("{0:d2}{1:d2}{2:d2}{3:d2}{4:d2}{5:d2}{6:d3}",
                dt.Year - 2000,
                dt.Month,
                dt.Day,
                dt.Hour,
                dt.Minute,
                dt.Second,
                dt.Millisecond);
            return name;
        }

        public static string GetFileName(this Frame f)
        {
            DateTime dt = DateTime.FromBinary(f.timeStamp);
            string name = string.Format(@"{0:d2}_{1}.jpg", f.cameraID, dt.ToFileName());
            return name;
        }
    }
}
