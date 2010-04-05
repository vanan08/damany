using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.PC.Domain
{
    public class CameraInfo
    {
        public int Id { get; set; }
        public Uri Location { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CameraProvider Provider { get; set; }
        public bool Enabled { get; set; }
    }
}
