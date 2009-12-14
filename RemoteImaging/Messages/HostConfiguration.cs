using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.RemoteImaging.Net.Discovery
{
    public class HostConfiguration
    {
        public string Name { get; set; }
        public string ip { get; set; }
        public int CameraID { get; set; }
        public object ID 
        {
            get
            {
                return this.Name;
            }
        }
    }
}
