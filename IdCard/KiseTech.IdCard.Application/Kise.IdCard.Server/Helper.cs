﻿using System;
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
            if (!xmlString.StartsWith("<?xml")) return new QueryResult() { Error = new Exception(xmlString) };

            try
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
            catch (Exception ex)
            {
                return new QueryResult() { Error = new FormatException("Invalid XML format", ex) };
            }
        }



        private static Kise.IdCard.Server.QueryResult ParseQueryResult(List<System.Xml.Linq.XElement> rows)
        {
            if (rows == null)
                throw new ArgumentNullException("rows", "rows is null.");


            var qr = new Kise.IdCard.Server.QueryResult();

            var idspecs = new List<IdSpec>();
            var propertyNames = rows[1].Elements("Data").ToList();
            for (int i = 2; i < propertyNames.Count; i++)
            {
                var idSpec = ParseOne(rows[1], rows[i]);
                idspecs.Add(idSpec);
            }
            qr.IdInfos = idspecs.ToArray();
            return qr;
        }
        private static IdSpec ParseOne(XElement template, XElement data)
        {

            var spec = new IdSpec();

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
                        spec.BornDate = ParseDatetime(v);
                        break;
                    case "SFZH":
                        spec.IdNo = v;
                        break;
                    case "XB":
                        spec.SexCode = IsMale(v) ? "1" : "2";
                        break;
                    case "XP":
                        spec.ImageData = Convert.FromBase64String(v);
                        break;
                    case "FWCS":
                        spec.Employer = v;
                        break;
                    case "MZ":
                        spec.MinorityCode = v;
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


        private static DateTime ParseDatetime(string dateString)
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