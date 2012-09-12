using System;
using Kise.IdCard.Messaging.Link;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Kise.IdCard.Messaging.Test
{
    [TestClass]
    public class SmsLinkTest
    {
        [TestMethod]
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

            var ep = new CellPhoneEndPoint() {CellNumber = "15928044631"};
            smsLink.SendAsync(ep, "0000"); //15928044631

            waitFor.WaitOne(TimeSpan.FromSeconds(60));

            Assert.IsTrue(receivedResponse);
            Assert.IsNotNull( msgReceived );
            System.Diagnostics.Debug.WriteLine(msgReceived);

        }
    }
}
