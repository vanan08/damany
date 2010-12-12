using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Damany.RemoteImaging.Common;

namespace RemoteImaging
{
    public class ImageCapturedEventArgs : EventArgs
    {
        public Frame FrameCaptured { get; set; }
    }
}
