using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util
{
    public static class Helper
    {
        public static DateTime ParseDatetime(string dateString)
        {
            if (String.IsNullOrEmpty(dateString) || dateString.Length < 8)
                throw new ArgumentException("dateString is null or empty.", "dateString");

            int y = int.Parse(dateString.Substring(0, 4));
            int m = int.Parse(dateString.Substring(4, 2));
            int d = int.Parse(dateString.Substring(6, 2));

            return new DateTime(y, m, d);

        }

    }
}
