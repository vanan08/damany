using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;
using MiscUtil;

namespace Damany.Imaging.PlugIns
{
    public class FaceComparer : Common.IPortraitHandler
    {
        public void Initialize(IEn)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void HandlePortraits(IList<Frame> motionFrames, IList<Portrait> portraits)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public string Description
        {
            get { throw new NotImplementedException(); }
        }

        public string Author
        {
            get { throw new NotImplementedException(); }
        }

        public Version Version
        {
            get { throw new NotImplementedException(); }
        }

        public bool WantCopy
        {
            get { throw new NotImplementedException(); }
        }

        public bool WantFrame
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<EventArgs<Exception>> Stopped;
    }
}