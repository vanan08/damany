using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Kise.IdCard.Messaging.Test
{
    [TestClass]
    public class TcpLinkTest
    {
        [TestMethod]
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
            var ep = new IPEndPoint(IPAddress.Loopback, 1000);
            client.SendAsync(ep, "hi there");

            System.Threading.Thread.Sleep(2000);
            Assert.AreEqual(serverReceiveMessage.Message, "hi there");
            Assert.AreEqual(serverReplyMessage.Message, "bitch");


        }
    }
}
