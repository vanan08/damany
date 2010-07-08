﻿using System;
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
            using (Damany.Cameras.Wrappers.AipStarCamera cam =
                new Damany.Cameras.Wrappers.AipStarCamera("192.168.1.204", 6002, "system", "system"))
            {
                cam.Connect();

                System.Threading.Thread.Sleep(3000);

                int count = 0;

                while (count < 500)
                {
                    ++count;
                    var frame = cam.RetrieveFrame();
                    System.Diagnostics.Debug.WriteLine(frame.ToString());
                }

            }
        }
    }
}
