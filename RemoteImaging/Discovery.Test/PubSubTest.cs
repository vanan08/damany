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

        System.Threading.AutoResetEvent go = new System.Threading.AutoResetEvent(false);

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
                
                pub.Start();
                sub.Start();

                sub.Subscribe(topic, this.handler);

                pub.Publish(topic, toPub, 3000);

                System.Diagnostics.Debug.WriteLine("publish: " + toPub.ToString());

                go.WaitOne(3000);

                Assert.AreEqual(received, toPub);

            }
        }

        object received;

        private void handler(object sender,  TopicArgs args)
        {
            received = args.DataObject;
            System.Net.IPEndPoint ep = sender as System.Net.IPEndPoint;
           
            System.Diagnostics.Debug.WriteLine("received: from: " + args.From.ToString() + " Topic: " + args.Topic.ToString() + " msg: "  + received.ToString());
            go.Set();

        }
    }

}
