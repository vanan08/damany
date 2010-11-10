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

            var waitFor =
            new System.Threading.AutoResetEvent(false);
            bool receivedResponse = false;

            var smsService = new SmsService("COM3", 9600);
            var task = smsService.QueryAsync("10086", "0000");

            var response = task.Result;

            System.Diagnostics.Debug.WriteLine(response);
            receivedResponse = true;
            waitFor.Set();


        }
    }
}
