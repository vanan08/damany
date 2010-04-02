using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.PlugIns
{
    public class PersonOfInterestDetectionResult
    {
        public PersonOfInterest Details { get; set; }
        public Common.Portrait Portrait { get; set; }
        public float Similarity { get; set; }
    }
}
