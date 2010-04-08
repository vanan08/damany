using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging
{
    public class Camera
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string IpAddress { get; set; }

        public string Mac { get; set; }

        public bool Status { get; set; }
    }
}
