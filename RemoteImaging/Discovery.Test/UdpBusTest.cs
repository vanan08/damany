using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Damany.RemoteImaging.Net.Discovery;

namespace Discovery.Test
{
    class UdpBusTest
    {
        [Test]
        [Row("cool", "ok")]
        public void DoTest(string topic, object data)
        {
            using (UdpBus bus = new UdpBus("224.0.0.23", 40001))
            {

                object received = null;

                bus.Subscrib(topic, (o, e) =>
                   {
                       System.Diagnostics.Debug.WriteLine(e.DataObject.ToString());
                       received = e.DataObject;
                      Assert.AreEqual(data, received);
                   });

                bus.Start();

                bus.Publish(topic, data, 3000);

                System.Threading.Thread.Sleep(3000);
            }
        }
    }
}
