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

            camera.IPAddress = "192.168.1.200";
            camera.Connect();
        }

        [Test]
        [Row(1)]
        [Row(0)]
        public void CaptureImageBytesTest(int i)
        {
            var bytes = this.camera.CaptureImageBytes();

            var img = Image.FromStream(new System.IO.MemoryStream(bytes));

            Assert.IsNotNull(img);
        }

        [Test]
        [Row(1)]
        [Row(0)]
        [Row(4)]
        public void ShutterSpeedPropertyTest(int speed)
        {

            ShutterMode shutterMode = ShutterMode.Short;
            int shutterSpeed = 0;
            this.camera.SetShutterSpeed(shutterMode, shutterSpeed);
            this.camera.UpdateProperty();
            Assert.IsTrue(shutterMode == this.camera.ShutterMode && shutterSpeed == this.camera.ShortShutterLevel);

            IrisMode irisMode = IrisMode.Manual;
            int irisLevel = 0;
            this.camera.SetIrisLevel(irisMode, irisLevel);
            this.camera.UpdateProperty();
            Assert.IsTrue(irisMode == this.camera.IrisMode && irisLevel == this.camera.ManualIrisLevel);

            bool enableAgc = true;
            bool enableDigitalGain = true;
            this.camera.SetAgc(enableAgc, enableDigitalGain);
            this.camera.UpdateProperty();

            Assert.IsTrue(enableAgc == this.camera.AgcEnabled && enableDigitalGain == this.camera.DigitalGainEnabled);

        }

        [Test]
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



        [FixtureTearDown]
        public void ResetCamera()
        {
            this.camera.SetIrisLevel(IrisMode.Manual, 50);
            var clock = new Clock();
            //clock.ThreadSleep(15000);
            this.camera.SetShutterSpeed(ShutterMode.Short, 1);
            //clock.ThreadSleep(15000);
            this.camera.SetAgc(true, false);
            //clock.ThreadSleep(15000);
        }


    }
}
