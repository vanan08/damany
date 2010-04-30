using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace Damany.Util.Extensions
{
    public static class MiscHelper
    {
        // usage: someObject.AsEnumerable();
        public static IEnumerable<T> AsEnumerable<T>(this T item)
        {
            yield return item;
        }

        public static DateTime Parse(this string txt)
        {
            int y = int.Parse(txt.Substring(0, 4));
            int m = int.Parse(txt.Substring(4, 2));
            int d = int.Parse(txt.Substring(6, 2));
            int h = int.Parse(txt.Substring(8, 2));
            int min = int.Parse(txt.Substring(10, 2));
            int sec = int.Parse(txt.Substring(12, 2));

            return new DateTime(y, m, d, h, min, sec, 0);
        }


        public static Image FromFileBuffered(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return Image.FromStream(fs);
            }
        }

        public static void CopyPropertiesTo<T>(this T source, T dest)
        {
            var plist = from prop in typeof(T).GetProperties() where prop.CanRead && prop.CanWrite select prop;

            foreach (PropertyInfo prop in plist)
            {
                prop.SetValue(dest, prop.GetValue(source, null), null);
            }
        }

        public static void Dispose(this IList<IDisposable> disposables)
        {
            if (disposables != null)
            {
                foreach (var item in disposables)
                {
                    item.Dispose();
                }
            }
        }

        public static DateTime RoundToMinute(this DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day,
                                time.Hour, time.Minute, 0, time.Kind);
        }



    }
}
