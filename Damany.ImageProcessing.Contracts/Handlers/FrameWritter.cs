using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Handlers
{
    public class FrameWritter : Damany.Imaging.Common.IMotionFrameHandler
    {

        #region IMotionFrameHandler Members

        public void HandleMotionFrame(IList<Damany.Imaging.Common.Frame> motionFrames)
        {
            motionFrames.ToList().ForEach(frame =>
            {
                frame.GetImage().SaveImage(frame.Guid.ToString() + ".jpg");
                frame.Dispose();
            });

        }

        #endregion
    }
}
