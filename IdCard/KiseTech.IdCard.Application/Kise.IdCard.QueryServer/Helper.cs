using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Kise.IdCard.Server
{
    public class Helper
    {
        public static Model.IdCardInfo[] Parse(string xmlString)
        {
            var doc = XDocument.Parse(xmlString);
            var rows = doc.Descendants("Row").ToList();
            if (rows.Count < 3)
            {
                return new Model.IdCardInfo[0];
            }

            var qr = ParseQueryResult(rows);
            return qr;

        }



        private static Model.IdCardInfo[] ParseQueryResult(List<System.Xml.Linq.XElement> rows)
        {
            if (rows == null)
                throw new ArgumentNullException("rows", "rows is null.");

            var idspecs = new List<IdCard.Model.IdCardInfo>();
            var propertyNames = rows[1].Elements("Data").ToList();
            for (int i = 2; i < rows.Count; i++)
            {
                var idSpec = ParseOne(rows[1], rows[i]);
                idspecs.Add(idSpec);
            }
            return idspecs.ToArray();
        }

        private static IdCard.Model.IdCardInfo ParseOne(XElement template, XElement data)
        {

            var spec = new IdCard.Model.IdCardInfo();

            var names = template.Elements("Data").ToList();
            var values = data.Elements("Data").ToList();
            for (int i = 0; i < names.Count(); i++)
            {
                var n = names[i].Value;
                if (string.IsNullOrEmpty(n)) continue;
                var v = values[i].Value;
                if (string.IsNullOrEmpty(v)) continue;

                n = n.ToUpperInvariant();
                switch (n)
                {
                    case "XM":
                        spec.Name = v;
                        break;
                    case "CSRQ":
                        spec.BornDate = Util.Helper.ParseDatetime(v);
                        break;
                    case "SFZH":
                        spec.IdCardNo = v;
                        break;
                    case "XB":
                        spec.SexCode = IsMale(v) ? 1 : 2;
                        break;
                    case "XP":
                        spec.PhotoData = Convert.FromBase64String(v);
                        break;
                    //case "FWCS":
                    //    spec.Employer = v;
                    //    break;
                    case "MZ":
                        spec.MinorityCode = int.Parse(v);
                        break;
                    default:
                        break;
                }
            }

            return spec;

        }
        private static bool IsMale(string sexString)
        {
            return sexString.Contains("男") || sexString == "1";
        }


    }
}
