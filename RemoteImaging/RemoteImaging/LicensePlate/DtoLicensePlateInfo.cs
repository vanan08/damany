using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public class DtoLicensePlateInfo
    {
        public DateTime CaptureTime { get; set; }
        public string LicensePlateNumber { get; set; }
        public int CapturedFrom { get; set; }
        public string LicensePlateImageFilePath { get; set; }

    }
}
