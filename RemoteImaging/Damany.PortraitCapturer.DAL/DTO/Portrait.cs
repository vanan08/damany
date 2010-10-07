﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;

namespace Damany.PortraitCapturer.DAL.DTO
{
    public class Portrait
    {
        public DateTime CapturedAt { get; set; }
        public int SourceId { get; set; }
        public System.Guid Guid { get; set; }

        public OpenCvSharp.CvRect FaceBounds { get; set; }
        public Frame Frame { get; set; }
        public string Path { get; set; }
    }
}
