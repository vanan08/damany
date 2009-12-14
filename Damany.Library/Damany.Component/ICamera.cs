using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Damany.Component
{
    public interface ICamera
    {
        void Connect();
        bool Record { get; set; }
        void Start();


        Image CaptureImage();
        byte[] CaptureImageBytes();
    }
}
