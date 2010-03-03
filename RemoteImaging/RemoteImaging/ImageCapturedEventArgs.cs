using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RemoteImaging
{
    public class ImageCapturedEventArgs : EventArgs
    {
        public Image ImageCaptured { get; set; }
    }
}
