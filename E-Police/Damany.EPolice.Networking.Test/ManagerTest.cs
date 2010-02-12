using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using Damany.EPolice.Networking.Simulator;

namespace Damany.EPolice.Networking.Test
{
    [TestFixture]
    public class ManagerTest
    {
        private Worker worker;
        [FixtureSetUp]
        public void Setup()
        {
            Configuration.Encoding = System.Text.Encoding.Unicode;
            Configuration.RemoteIp = "127.0.0.1";
            Configuration.RemotePort = 10000;

            worker = new Simulator.Worker();
            worker.Start();
        }


        [Test]
        public void Test()
        {
            System.Threading.AutoResetEvent evt = new System.Threading.AutoResetEvent(false);
            Packets.LicensePlatePacket p = null;
            int count = 0;

            Manager mnger = new Manager(new PacketSplitter());
            var parser = new Parsers.LicensePlateParser();
            parser.Handler += licensePlate =>
            {
                System.Diagnostics.Debug.WriteLine(licensePlate.LicensePlate.LicenseNumber);
                p = licensePlate;
                ++count;
                if (count > 1000)
                {
                    evt.Set();
                }
                
            };
            mnger.Parsers.Add(parser);

            mnger.Start();
            evt.WaitOne(10000);

            Assert.IsNotNull(p);
        }
    }
}
