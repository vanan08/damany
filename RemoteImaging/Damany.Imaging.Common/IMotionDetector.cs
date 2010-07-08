using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Common
{
    public interface IMotionDetector
    {
        bool Detect(Frame frame, ref MotionDetectionResult result);
    }
}
