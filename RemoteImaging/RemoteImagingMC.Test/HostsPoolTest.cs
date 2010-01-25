using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using MbUnit.Framework.Reflection;
using RemoteImaging;
using Damany.RemoteImaging.Common;
using System.Collections.Specialized;
using Damany.RemoteImaging.Net.Discovery;
using Damany.RemoteImaging.Net.Messages;

namespace RemogeImagingMC.Test
{
    [TestFixture]
    public class HostsPoolTest
    {
        private RemoteImaging.HostsPool pool;

        [SetUp]
        public void Setup()
        {
            this.pool = new RemoteImaging.HostsPool("224.0.0.23", 40001);

            this.pool.ListChanged +=new System.ComponentModel.ListChangedEventHandler(pool_ListChanged);
        }

        void pool_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.ListChangedType);
        }

        [Test]
        public void PubSubTest()
        {
            UdpBus bus = (UdpBus)Reflector.GetField(this.pool, "bus");
            
            object received = null;

            System.Threading.AutoResetEvent evt = new System.Threading.AutoResetEvent(false);

            this.pool.Start();

            HostConfiguration hostSpec = new HostConfiguration(0){ CameraID = 2, Name = "mike", };

            bus.Publish(Topics.HostReply, hostSpec, 3000);

            System.Threading.Thread.Sleep(6000);

            var host = this.pool[hostSpec.StationID];

            Assert.IsNotNull(host);

            Assert.AreEqual("mike", host.Config.Name);

            hostSpec.Name = "jack";

            bus.Publish(Topics.HostReply, hostSpec, 3000);

            System.Threading.Thread.Sleep(6000);

            host = this.pool[hostSpec.StationID];
            Assert.AreEqual("jack", host.Config.Name);

        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ContainsIDTest()
        {
            HostConfiguration config = new HostConfiguration(0) {  CameraID = 3,  Name = "abc" };
            var host = new Host{ Config = config, Status = HostStatus.OnLine, LastSeen = DateTime.Now,};

            HostConfiguration config1 = new HostConfiguration(0) { CameraID = 4, Name = "abc" };
            var host1 = new Host { Config = config, Status = HostStatus.OnLine, LastSeen = DateTime.Now, };

            pool.Add(host);

            Assert.IsTrue(pool.ContainsID(host.Config.StationID));
            Assert.IsTrue(pool.Contains(host1));


            Assert.IsFalse(pool.ContainsID(4));

            var h = pool[config.StationID];

            Assert.IsNotNull(h);

            h.Config.Name = "benny";
            h.LastSeen = DateTime.Now;

            h = pool[config.StationID];

            Assert.AreEqual("benny", h.Config.Name);

            pool.Add(host1);
        }



        [TearDown]
        public void TearDown()
        {
            if (this.pool != null) this.pool.Dispose();
        }
    }
}
