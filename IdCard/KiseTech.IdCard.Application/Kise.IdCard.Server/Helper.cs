using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Kise.IdCard.Server
{
    public class Helper
    {
        public static Kise.IdCard.Server.QueryResult Parse(string xmlString)
        {
            var doc = XDocument.Parse(xmlString);
            var rows = doc.Descendants("Row").ToList();
            if (rows.Count < 3)
            {
                return new Kise.IdCard.Server.QueryResult();
            }

            var qr = ParseQueryResult(rows);
            return qr;
        }

        private static Kise.IdCard.Server.QueryResult ParseQueryResult(List<System.Xml.Linq.XElement> rows)
        {
            var qr = new Kise.IdCard.Server.QueryResult();
            var propertyNames = rows[1].Elements("Data").ToList();
            var propertyValues = rows[2].Elements("Data").ToList();
            for (int i = 0; i < propertyNames.Count; i++)
            {
                var n = propertyNames[i].Value;
                if (string.IsNullOrEmpty(n)) continue;
                var v = propertyValues[i].Value;
                if (string.IsNullOrEmpty(v)) continue;

                n = n.ToUpperInvariant();
                switch (n)
                {
                    case "XM":
                        qr.IdInfo.Name = v;
                        break;
                    case "CSRQ":
                        qr.IdInfo.BornDate = DateTime.Parse(v);
                        break;
                    case "SFZH":
                        qr.IdInfo.IdNo = v;
                        break;
                    case "XB":
                        qr.IdInfo.SexCode = IsMale(v) ? "1" : "2";
                        break;
                    default:
                        break;
                }
            }
            return qr;
        }
        private static bool IsMale(string sexString)
        {
            return sexString.Contains("男") || sexString == "1";
        }

    }
}
