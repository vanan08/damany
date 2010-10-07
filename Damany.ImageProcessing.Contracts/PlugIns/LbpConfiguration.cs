using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Damany.Imaging.PlugIns
{
    public class LbpConfiguration
    {
        [Description("识别阈值")]
        public int Threshold { get; set; }
    }
}
