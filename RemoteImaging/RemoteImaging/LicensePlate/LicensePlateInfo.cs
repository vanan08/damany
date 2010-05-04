using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public class LicensePlateInfo
    {
        public LicensePlateInfo()
        {
            Guid = System.Guid.NewGuid();
        }

        public DateTime CaptureTime { get; set; }
        public string LicensePlateNumber { get; set; }
        public int CapturedFrom { get; set; }
        public Guid Guid { get; set; }

        public string LicensePlateImageFileAbsolutePath { get; set; }

        private byte[] _imageData;
        public byte[] ImageData
        {
            get
            {
                if (_imageData == null)
                {
                    _imageData = System.IO.File.ReadAllBytes(LicensePlateImageFileAbsolutePath);
                }
                return _imageData;
            }
            set
            {
                _imageData = value;
            }
        }

        public System.Drawing.Image LoadImage()
        {
            using (var stream = new System.IO.MemoryStream(ImageData))
            {
                return System.Drawing.Image.FromStream(stream);
            }
        }
    }
}
