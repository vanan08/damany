using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.Core
{
    public class Video
    {
        public string Path { get; set; }
        public DateTime CapturedAt { get; set; }
        public bool HasFaceCaptured { get; set; }
        public bool HasMotionDetected { get; set; }
    }
}
