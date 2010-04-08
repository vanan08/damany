using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.PortraitCapturer.DAL;

namespace Damany.Imaging.Handlers
{
    public class PersistenceWriter : Damany.Imaging.Common.IPortraitHandler
    {
        private Damany.PortraitCapturer.DAL.IRepository repository;
        #region IPortraitHandler Members

        public PersistenceWriter(Damany.PortraitCapturer.DAL.IRepository repository)
        {
            this.repository = repository;
        }

        public void Initialize() {}

        public void Start() {}

        public void HandlePortraits(IList<Damany.Imaging.Common.Frame> motionFrames, IList<Damany.Imaging.Common.Portrait> portraits)
        {
            motionFrames.ToList().ForEach(f =>{ this.repository.SaveFrame(f); });
            portraits.ToList().ForEach(p => { this.repository.SavePortrait(p); });
            
        }

        public void Stop()  { }

        public string Name
        {
            get { return "portrait saver"; }
        }

        public string Description
        {
            get { return "Default persistent"; }
        }

        public string Author
        {
            get { return "Damany"; }
        }

        public Version Version
        {
            get { throw new NotImplementedException(); }
        }

        public bool WantCopy
        {
            get { return false; }
        }

        public bool WantFrame
        {
            get { return true; }
        }

        public event EventHandler<MiscUtil.EventArgs<Exception>> Stopped;

        #endregion

    }
}
