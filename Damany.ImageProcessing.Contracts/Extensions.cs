using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.ImageProcessing.Contracts
{
    public static class Extensions
    {
        public static void Dispose(this IList<MotionFrame> motionFrames)
        {
            if (motionFrames != null)
            {
                foreach (var item in motionFrames)
                {
                    item.Dispose();
                }
            }
        }
    }
}
