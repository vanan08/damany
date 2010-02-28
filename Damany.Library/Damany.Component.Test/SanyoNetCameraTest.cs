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

        private void CaptureFrame()
        {
            var bytes = this.camera.CaptureImageBytes();

            var img = Image.FromStream(new System.IO.MemoryStream(bytes));

            Assert.IsNotNull(img);
        }


        public void CaptureImageBytesTest(int i)
        {
            CaptureFrame();
        }

        [Test]
        [Repeat(15)]
        public void ReconnectTest()
        {
            System.Threading.Thread.Sleep(3000);
            this.camera.Connect();
            this.CaptureFrame();
        }

        public void ShutterSpeedPropertyTest(int speed)
        {

            ShutterMode shutterMode = ShutterMode.Short;
            int shutterSpeed = 0;
            this.camera.SetShutter(shutterMode, shutterSpeed);
            this.camera.UpdateProperty();
            Assert.IsTrue(shutterMode == this.camera.ShutterMode && shutterSpeed == this.camera.ShortShutterLevel);

            IrisMode irisMode = IrisMode.Manual;
            int irisLevel = 0;
            this.camera.SetIris(irisMode, irisLevel);
            this.camera.UpdateProperty();
            Assert.IsTrue(irisMode == this.camera.IrisMode && irisLevel == this.camera.ManualIrisLevel);

            bool enableAgc = true;
            bool enableDigitalGain = true;
            this.camera.SetAGCMode(enableAgc, enableDigitalGain);
            this.camera.UpdateProperty();

            Assert.IsTrue(enableAgc == this.camera.AgcEnabled && enableDigitalGain == this.camera.DigitalGainEnabled);

        }

        public void IrisPropertyTest(int level)
        {
            this.camera.SetIris(IrisMode.Manual, level);

            var clock = new Clock();
            clock.ThreadSleep(5000);
        }

        public void AgcTest(bool enableAgc, bool enableDigitalGain)
        {
            this.camera.SetAGCMode(enableAgc, enableDigitalGain);
        }



        public void ResetCamera()
        {
            this.camera.SetIris(IrisMode.Manual, 50);
            var clock = new Clock();
            //clock.ThreadSleep(15000);
            this.camera.SetShutter(ShutterMode.Short, 1);
            //clock.ThreadSleep(15000);
            this.camera.SetAGCMode(true, false);
            //clock.ThreadSleep(15000);
        }


    }
}
