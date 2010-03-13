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
        public int SourceId { get; set; }
        public System.Guid Guid { get; set; }
        public List<OpenCvSharp.CvRect> FacesBounds { get; set; }
        public string Path { get; set; }
    }
}
