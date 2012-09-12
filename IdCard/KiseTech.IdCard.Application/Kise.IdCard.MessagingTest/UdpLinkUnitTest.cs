using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kise.IdCard.Messaging.Test
{
    [TestClass]
    public class UdpLinkUnitTest
    {
        [TestMethod]
        public void ReadSuccessTest()
        {
            var serverPort = 10000;
            var expectedMessage = "afdfdfdfdfd";

            var server = new Link.UdpServer(serverPort);
            server.NewMessageReceived += (sender, args) =>
                                             {
                                                 expectedMessage = DateTime.Now.ToString("HH:ss.fff");
                                                 server.SendAsync(args.Value.Sender, expectedMessage);
                                             };
            server.Start();

            var serverEp = new IPEndPoint(IPAddress.Loopback, serverPort);
            var client = new Link.UdpClient(serverEp);
            var result = client.SendAsync(null, "def");
            var reply = result.Result;

            Assert.AreEqual(reply.Message, expectedMessage);
        }


        [TestMethod]
        [ExpectedException(typeof(System.Net.Sockets.SocketException))]
        public void ReadTimeOut()
        {
            var server = new IPEndPoint(IPAddress.Loopback, 8888);
            var client = new Link.UdpClient(server);
            try
            {
                var reply = client.SendAsync(null, "abce");
                var result = reply.Result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw;
            }
            
        }
    }
}
