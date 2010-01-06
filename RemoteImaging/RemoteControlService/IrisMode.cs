using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace RemoteControlService
{
    [DataContract]
    public enum IrisMode
    {
        Auto, Manual,
    }
}
