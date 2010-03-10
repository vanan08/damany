using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Contracts
{
    public interface IPortraitHandler
    {
        void Initialize();
        void Start();
        void HandlePortraits(IList<Frame> motionFrames, IList<Portrait> portraits);
        void Stop();

        string Name { get; }
        string Description { get; }
        string Author { get; }
        Version Version { get; }

        bool WantCopy { get; }
        bool WantFrame { get; }

        event EventHandler< MiscUtil.EventArgs<Exception> > Stopped;
    }
}
