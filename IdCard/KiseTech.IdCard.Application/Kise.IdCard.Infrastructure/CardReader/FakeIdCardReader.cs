using System;
using System.Threading.Tasks;
using Kise.IdCard.Messaging;

namespace Kise.IdCard.Infrastructure.CardReader
{
    public class FakeIdCardReader : IIdCardReader
    {
        private Random _random = new Random();
        int count = 0;

        public FakeIdCardReader()
        {
        }

        public async Task<IdInfo> ReadAsync()
        {
            var random = new Random(DateTime.Now.Millisecond);

            await
            TaskEx.Delay(3000);

            IdInfo v = new IdInfo()
                           {
                               Address = "四川成都",
                               BornDate = "19781231",
                               GrantDept = "四川省成都市青羊分局",
                               IdCardNo = "510403197309112610",
                               Minority = random.Next(1, 56).ToString(),
                               Name = "张三",
                               PhotoData = Properties.Resources.Image0001,
                               Sex = random.Next(1, 2).ToString(),
                               ValidateFrom = "19781231",
                               ValidateUntil = "20121231",

                           };

            ++count;

            if (count % _random.Next(4) == 0) throw new Exception();

            return v;
        }
    }
}