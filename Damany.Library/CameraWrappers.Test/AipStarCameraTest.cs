using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

namespace CameraWrappers.Test
{
    [TestFixture]
    public class AipStarCameraTest
    {
        [Test]
        public void Test()
        {
            Damany.Component.CameraWrappers.AipStarCamera cam =
                new Damany.Component.CameraWrappers.AipStarCamera("192.168.1.204", 6002, "system", "system");
            cam.Connect();

            int count = 0;

            while (true)
            {
                ++count;
                var bytes = cam.CaptureImageBytes();

                if (bytes.Length > 0)
                {
                    System.Diagnostics.Debug.WriteLine("captured: " + count.ToString());
                    System.IO.File.WriteAllBytes("img.jpg", bytes);
                    break;
                }
            }

            cam.Dispose();
        }
    }
}
