﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.RemoteImaging.Common
{
    [Serializable]
    public class Frame
    {
        public System.Drawing.Image image;
        public DateTime timeStamp;
        public int CameraID;
    }

    [Serializable]
    public class Portrait
    {
        public System.Drawing.Image image;
        public DateTime timeStamp;
        public int cameraId;
    }
}
