using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Damany.RemoteImaging.Net.Discovery;

namespace Discovery.Test
{
    class StringArgs : EventArgs
    {
        public string Msg { get; set; }

        public StringArgs(string msg)
        {
            this.Msg = msg;
        }
    }

    public class PubSubTest
    {
        int sendCount = 0;
        int recvCount = 0;

        [Test]
        public void PubSub()
        {
            string ip = "224.0.0.23";
            int port = 40001;

            Publisher pub = new Publisher(ip, port);
            Subscriber sub = new Subscriber(ip, port);

            const string topic = "test topic";
            sub.Subscribe<string>(topic, this.handler);

            pub.Start();
            sub.Start();

            pub.Publish(topic, "hi there", 3000);
            sendCount++;
            System.Diagnostics.Debug.WriteLine("publish topic: " + topic);

            System.Threading.Thread.Sleep(3000);

            Assert.AreEqual(sendCount, recvCount);
        }

        private void handler(string msg)
        {
            recvCount++;
            System.Diagnostics.Debug.WriteLine("received: " + msg);

        }
    }
}
