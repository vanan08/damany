using System;
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

        public static IdCardInfo ToModelIdCardInfo(this Infrastructure.CardReader.IdInfo info)
        {
            var v = new Model.IdCardInfo()
                        {
                            Address = info.Address,
                            BornDate = info.BornDate.ParseIntoDateTime(),
                            GrantDept = info.GrantDept,
                            IdCardNo = info.IdCardNo,
                            MinorityCode = int.Parse(info.Minority),
                            Name = info.Name,
                            SexCode = int.Parse(info.Sex),
                            ValidateFrom = info.ValidateFrom.ParseIntoDateTime(),
                            ValidateUntil = info.ValidateUntil.ParseIntoDateTime(),
                            PhotoData =  info.PhotoData
                        };

            return v;
        }

        public static string GetIdStatusName(IdStatus statusCode)
        {
            switch (statusCode)
            {
                case IdStatus.Normal:
                    return "正常";
                case IdStatus.PrisonBreaker:
                    return "逃犯";
                case IdStatus.UnKnown:
                    return "未知";
                case IdStatus.Wanted:
                    return "通缉犯";
                case IdStatus.WasLawBreaker:
                    return "刑满释放";
            }

            return "未知";
        }
    }
}
