using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Contracts;

namespace Damany.PortraitCapturer.DAL.DTO
{
    public class Frame
    {
        public DateTime CapturedAt { get; set; }
        public int CapturedFrom { get; set; }
        public System.Guid Guid { get; set; }
        public List<PortraitBounds> Portraits { get; set; }
        public string Path { get; set; }
    }
}
