using System;
using System.Threading.Tasks;
using Kise.IdCard.Model;

namespace Kise.IdCard.Infrastructure.CardReader
{
    public class FakeIdCardReader : IIdCardReader
    {
        private Random _random = new Random();
        int count = 0;

        public FakeIdCardReader()
        {
        }

        public async Task<Model.IdCardInfo> ReadAsync()
        {
            var random = new Random(DateTime.Now.Millisecond);

            await
            TaskEx.Delay(3000);

            var v = new IdCardInfo()
                           {
                               Address = "四川成都",
                               BornDate = "19730911".ParseIntoDateTime(),
                               GrantDept = "四川省成都市青羊分局",
                               IdCardNo = count % 2 == 0 ? "532627198502181580" : "510403197309112610",
                               MinorityCode = 1,
                               Name = "沈斌",
                               PhotoData = Properties.Resources.Image0001,
                               SexCode = 1,
                               ValidateFrom = "19781231".ParseIntoDateTime(),
                               ValidateUntil = "20121231".ParseIntoDateTime(),

                           };

            ++count;

            //if (count % _random.Next(4) == 0) throw new Exception();

            return v;
        }
    }
}