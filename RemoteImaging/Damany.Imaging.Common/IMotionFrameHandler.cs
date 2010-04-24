using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Common
{
    public interface IMotionFrameHandler
    {
        void HandleMotionFrame(IList<Frame> motionFrames);
    }
}
