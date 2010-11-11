using System;

namespace Kise.IdCard.Model
{
    public static class Converter
    {
        public static IdCardInfo ToModelIdCardInfo(this Infrastructure.CardReader.IdInfo info)
        {
            var v = new Model.IdCardInfo()
                        {
                            Address = info.Address,
                            //BornDate = DateTime.Parse(info.BornDate),
                            GrantDept = info.GrantDept,
                            IdCardNo = info.IdCardNo,
                            Name = info.Name,
                            //ValidateFrom = DateTime.Parse(info.ValidateFrom),
                            //ValidateUntil = DateTime.Parse(info.ValidateUntil),
                            PhotoFilePath = info.PhotoFilePath
                        };

            return v;
        }
    }
}
