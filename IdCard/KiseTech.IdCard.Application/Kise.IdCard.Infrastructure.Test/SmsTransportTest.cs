using System;
using System.Collections.Generic;
using System.Text;
using Gallio.Framework;
using Kise.IdCard.Messaging;
using Kise.IdCard.Messaging.Link;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

namespace Kise.IdCard.Infrastructure.Test
{
    [TestFixture]
    public class SmsTransportTest
    {
        [Test]
        public void Test()
        {
            //
            // TODO: Add test logic here
            //

            var waitFor =
            new System.Threading.AutoResetEvent(false);
            bool receivedResponse = false;

            var smsService = new SmsLink("COM3", 9600);
            smsService.SendAsync("10086", "0000");

            throw new NotImplementedException();
        }
    }
}
