using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Contracts
{
    public interface IPortraitHandler
    {
        void HandlePortraits(IList<Frame> motionFrames, IList<Portrait> portraits);
    }
}
