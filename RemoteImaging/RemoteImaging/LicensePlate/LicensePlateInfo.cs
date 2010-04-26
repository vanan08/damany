using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public class LicensePlateInfo
    {
        public DateTime CaptureTime { get; set; }
        public string LicensePlateNumber { get; set; }
        public int CapturedFrom { get; set; }
        public System.Drawing.Image LicensePlateImage { get; set; }
    }
}
