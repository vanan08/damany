using System;
using System.Collections.Generic;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

namespace Kise.IdCard.Messaging.Test
{
    [TestFixture]
    public class TcpLinkTest
    {
        [Test]
        public void Test()
        {
            //
            // TODO: Add test logic here
            //

            var serverReceiveMessage = new IncomingMessage();
            var serverReplyMessage = new IncomingMessage();

            var server = new Link.TcpServerLink(10000);
            server.Start();
            server.NewMessageReceived += (s, e) =>
                                             {
                                                 serverReceiveMessage = e.Value;
                                                 server.SendAsync(e.Value.Sender, "bitch");
                                             };

            var client = new Link.TcpClientLink();
            client.PortToConnect = 10000;
            client.Start();
            client.NewMessageReceived += (s, e) =>
                                             {
                                                 serverReplyMessage = e.Value;
                                             };

            System.Threading.Thread.Sleep(1000);
            client.SendAsync("", "hi there");

            System.Threading.Thread.Sleep(2000);
            Assert.AreEqual(serverReceiveMessage.Message, "hi there");
            Assert.AreEqual(serverReplyMessage.Message, "bitch");


        }
    }
}
