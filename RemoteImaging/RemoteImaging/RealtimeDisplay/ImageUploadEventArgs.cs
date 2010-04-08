using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemoteImaging.Core;

namespace RemoteImaging.RealtimeDisplay
{
    public class ImageUploadEventArgs
    {
        public int CameraID { get; set; }

        public ImageDetail[] Images { get; set; }
    }
}
