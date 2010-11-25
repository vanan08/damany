using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging
{
    public interface IFrameHandler
    {
        void HandleFrame(ManagedFrame f);
    }
}
