using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;
using System.ComponentModel.Composition;

namespace FaceCompareAlgorithmTester
{
    public class Controller
    {
        [ImportMany]
        public IEnumerable<ISimpleFaceComparer> FaceComparers { get; set; }
    }
}
