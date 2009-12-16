using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Damany.RemoteImaging.Net.Discovery;

namespace Discovery.Test
{
    [TestFixture]
    public class PubSubTest
    {
        int sendCount = 0;
        int recvCount = 0;

        [Test]
        [Row("hi, topic", 123)]
        [Row("abc", "jkf")]
        public void PubSub(string topic, object toPub)
        {
            string ip = "224.0.0.23";
            int port = 40001;

            using( Publisher pub = new Publisher(ip, port) )
            using( Subscriber sub = new Subscriber(ip, port) )
            {
                sub.Subscribe<object>(topic, this.handler);

                pub.Start();
                sub.Start();

                pub.Publish(topic, toPub, 3000);

                System.Diagnostics.Debug.WriteLine("publish: " + toPub.ToString());

                System.Threading.Thread.Sleep(3000);

                Assert.AreEqual(received, toPub);

            }


        }

        object received;

        private void handler(object msg)
        {
            received = msg;
           
            System.Diagnostics.Debug.WriteLine("received: " + msg.ToString());

        }
    }

    [Serializable]
    class StringArgs : EventArgs
    {
        public string Msg { get; set; }

        public StringArgs(string msg)
        {
            this.Msg = msg;
        }
    }
}
