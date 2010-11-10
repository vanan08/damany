using System;
using System.Collections.Generic;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

namespace Kise.IdCard.Infrastructure.Test
{
    using CardReader;
    using System.Threading.Tasks;

    [TestFixture]
    public class CardReaderTest
    {
        [Test]
        public void ReadSuccess()
        {
            //
            // TODO: Add test logic here
            //
            var info = ReadInfoAsync(1001).Result;

            Assert.IsNotNull(info.Address);
            Assert.IsNotNull(info.BornDate);
            Assert.IsNotNull(info.GrantDept);
            Assert.IsNotNull(info.IdCardNo);
            Assert.IsNotNull(info.Minority);
            Assert.IsNotNull(info.Name);
            Assert.IsNotNull(info.PhotoFilePath);
            Assert.IsNotNull(info.Sex);
            Assert.IsNotNull(info.ValidateFrom);
            Assert.IsNotNull(info.ValidateUntil);
        }


        [Test]
        [ExpectedException(typeof(Exception))]
        public void ReadReaderDisconnected()
        {
            var info = ReadInfoAsync(1001);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void ReadWithWrongPortNumber()
        {
            var info = ReadInfoAsync(9999);
            var result = info.Result;
        }

        private async Task<IdInfo> ReadInfoAsync(int port)
        {
            var reader = new IdCardReader(port);
            var info = await reader.ReadAsync();
            return info;
        }

    }
}
