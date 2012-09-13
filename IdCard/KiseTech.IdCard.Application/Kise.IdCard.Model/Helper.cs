﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kise.IdCard.Model
{
    public static class Helper
    {
        static Dictionary<int, string> SexDic = new Dictionary<int, string>();

        static Helper()
        {

        }

        public static string GetSexName(int sexCode)
        {
            if (sexCode == 1)
            {
                return "男";
            }

            return "女";
        }

        public static DateTime ParseIntoDateTime(this string s)
        {
            int y = int.Parse(s.Substring(0, 4));
            int m = int.Parse(s.Substring(4, 2));
            int d = int.Parse(s.Substring(6, 2));
            return new DateTime(y, m, d);
        }


    }
}