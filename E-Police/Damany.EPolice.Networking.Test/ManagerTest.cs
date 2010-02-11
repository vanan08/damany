using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

namespace Damany.EPolice.Networking.Test
{
    [TestFixture]
    public class ManagerTest
    {
        [FixtureSetUp]
        public void Setup()
        {
            Configuration.Encoding = System.Text.Encoding.Unicode;
            Configuration.RemoteIp = "127.0.0.1";
            Configuration.RemotePort = 10000;

        }


        [Test]
        public void Test()
        {
            System.Threading.AutoResetEvent evt = new System.Threading.AutoResetEvent(false);

            Manager mnger = new Manager();
            mnger.SubscribeLicensePlate(licensePlate =>
                {
                    System.Diagnostics.Debug.WriteLine(licensePlate.LicensePlate.LicenseNumber);
                    evt.Set();
                    
                });

            mnger.Start();


            evt.WaitOne(5000);
            
            
        }
    }
}
