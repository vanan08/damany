using System;
using Kise.IdCard.Messaging.Link;
using MbUnit.Framework;

namespace Kise.IdCard.Messaging.Test
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
