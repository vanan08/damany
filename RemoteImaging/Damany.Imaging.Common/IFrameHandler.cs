using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Common
{
    public interface IFrameHandler
    {
        void HandleFrame(Frame frameToHandle);
    }
}
