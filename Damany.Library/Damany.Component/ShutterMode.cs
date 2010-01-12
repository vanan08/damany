using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Component
{
    [System.Runtime.Serialization.DataContract]
    public enum ShutterMode
    {
        [System.Runtime.Serialization.EnumMember]
        Off,
        [System.Runtime.Serialization.EnumMember]
        Short,
        [System.Runtime.Serialization.EnumMember]
        Long,
    }
}
