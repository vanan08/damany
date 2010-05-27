using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;

namespace Damany.Imaging.PlugIns
{
    public class PersonOfInterestDetectionResult
    {
        public PersonOfInterest Details { get; set; }
        public Common.Portrait Portrait { get; set; }
        public float Similarity { get; set; }
    }
}
