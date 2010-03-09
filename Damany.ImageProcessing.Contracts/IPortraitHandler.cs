using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Contracts
{
    public interface IPortraitHandler
    {
        void Initialize();
        void HandlePortraits(IList<Frame> motionFrames, IList<Portrait> portraits);
        void Stop();

        bool WantCopy { get; }
    }
}
