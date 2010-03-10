using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Handlers
{
    public class FrameWritter : Imaging.Contracts.IMotionFrameHandler
    {

        #region IMotionFrameHandler Members

        public void HandleMotionFrame(IList<Damany.Imaging.Contracts.Frame> motionFrames)
        {
            motionFrames.ToList().ForEach(frame =>
            {
                frame.Ipl.SaveImage(frame.Guid.ToString() + ".jpg");
                frame.Dispose();
            });

        }

        #endregion
    }
}
