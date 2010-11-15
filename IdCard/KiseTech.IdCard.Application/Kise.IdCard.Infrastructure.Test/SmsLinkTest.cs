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
    public class SmsLinkTest
    {
        [Test]
        public void Test()
        {
            //
            // TODO: Add test logic here
            //

            var waitFor = new System.Threading.AutoResetEvent(false);
            bool receivedResponse = false;
            string msgReceived = null;

            var smsLink = new SmsLink("COM3", 9600);
            smsLink.Start();
            smsLink.NewMessageReceived += (s, e) =>
                                              {
                                                  receivedResponse = true;
                                                  msgReceived = e.Value.Message;
                                                  waitFor.Set();
                                              };
            smsLink.SendAsync("15928044631", "0000"); //15928044631

            waitFor.WaitOne(TimeSpan.FromSeconds(60));

            Assert.IsTrue(receivedResponse);
            Assert.IsNotNull( msgReceived );
            System.Diagnostics.Debug.WriteLine(msgReceived);

        }
    }
}
