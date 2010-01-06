using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using System.Drawing;
using Gallio.Common.Time;

namespace Damany.Component.Test
{
    [TestFixture]
    public class SanyoNetCameraTest
    {
        SanyoNetCamera camera = null;


        [FixtureSetUp]
        public void init()
        {
            camera = new SanyoNetCamera();
            camera.UserName = "guest";
            camera.Password = "guest";

            camera.IPAddress = "192.168.1.202";
            camera.Connect();
        }


        [Test]
        public void CaptureImageBytesTest()
        {
            var bytes = this.camera.CaptureImageBytes();

            var img = Image.FromStream(new System.IO.MemoryStream(bytes));

            Assert.IsNotNull(img);
        }

        [Row(1)]
        public void ShutterSpeedPropertyTest(int speed)
        {
            this.camera.SetShutterSpeed(ShutterMode.Short, speed);

            var clock = new Clock();
            clock.ThreadSleep(8000);
        }

        [Row(75)]
        public void IrisPropertyTest(int level)
        {
            this.camera.SetIrisLevel(IrisMode.Manual, level);

            var clock = new Clock();
            clock.ThreadSleep(5000);
        }

        [Test]
        [Row(true, true)]
        public void AgcTest(bool enableAgc, bool enableDigitalGain)
        {
            this.camera.SetAgc(enableAgc, enableDigitalGain);
        }



    }
}
