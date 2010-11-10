using System;
using System.Collections.Generic;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

namespace Kise.IdCard.Infrastructure.Test
{
    using CardReader;

    [TestFixture]
    public class CardReaderTest
    {
        [Test]
        public void ReadSuccess()
        {
            //
            // TODO: Add test logic here
            //
            IdInfo info = ReadInfo(1001);

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
            var info = ReadInfo(1001);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void ReadWithWrongPortNumber()
        {
            var info = ReadInfo(9999);
        }

        private IdInfo ReadInfo(int port)
        {
            var reader = new IdCardReader(port);
            return reader.Read();
        }

    }
}
