using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.Query
{
    [Flags]
    public enum SearchScope : byte
    {
        FaceCapturedVideo = 1,
        MotionWithoutFaceVideo = 2,
        MotionLessVideo = 4,
        All = byte.MaxValue,
    }
}
