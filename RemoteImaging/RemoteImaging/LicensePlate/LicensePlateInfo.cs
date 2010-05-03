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

        public string LicensePlateImageFileAbsolutePath { get; set; }
        public byte[] ImageData { get; set; }

        public System.Drawing.Image LoadImage()
        {
            if (ImageData == null)
            {
                ImageData = System.IO.File.ReadAllBytes(LicensePlateImageFileAbsolutePath);
            }

            using (var stream = new System.IO.MemoryStream(ImageData))
            {
                return System.Drawing.Image.FromStream(stream);
            }
            
        }
    }
}
