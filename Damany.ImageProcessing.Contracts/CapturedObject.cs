﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Contracts
{
    public class CapturedObject
    {
        public CapturedObject()
        {
            this.CapturedAt = DateTime.Now;
            this.Guid = System.Guid.NewGuid();
        }

        public DateTime CapturedAt { get; set; }
        public IFrameStream CapturedFrom { get; set; }
        public System.Guid Guid { get; set; }

    }
}