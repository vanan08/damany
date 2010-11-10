using System;
using System.Collections.Generic;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

namespace Kise.IdCard.Infrastructure.Test
{
    using Sms;

    [TestFixture]
    public class SmsServiceTest
    {
        [Test]
        public void Test()
        {
            //
            // TODO: Add test logic here
            //

            var smsService = new SmsService("com1", 9600);
        }
    }
}
