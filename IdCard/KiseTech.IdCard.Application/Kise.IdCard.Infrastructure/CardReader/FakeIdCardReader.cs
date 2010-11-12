using System;
using System.Threading.Tasks;

namespace Kise.IdCard.Infrastructure.CardReader
{
    public class FakeIdCardReader : IIdCardReader
    {
        public async Task<IdInfo> ReadAsync()
        {
            var random = new Random(DateTime.Now.Millisecond);

            await
            TaskEx.Delay(3000);

            IdInfo v = new IdInfo()
                           {
                               Address = "dashi road " + DateTime.Now.ToString(),
                               BornDate = "19781231",
                               GrantDept = "jinjiang police " + DateTime.Now.ToString(),
                               IdCardNo = "510403197309112610",
                               Minority = random.Next(1, 56).ToString(),
                               Name = "benny" + DateTime.Now.ToString(),
                               PhotoData = Properties.Resources.Image0001,
                               Sex = random.Next(1, 2).ToString(),
                               ValidateFrom = "19781231",
                               ValidateUntil = "20121231",

                           };
            return v;
        }
    }
}