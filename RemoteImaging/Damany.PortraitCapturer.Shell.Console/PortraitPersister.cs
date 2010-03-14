using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.PortraitCapturer.DAL;
using Damany.PortraitCapturer.DAL.Providers;

namespace Damany.PortraitCapturer.Shell.CmdLine
{
    class PortraitPersister : Damany.Imaging.Contracts.IPortraitHandler
    {
        private Repository.PersistenceService service;
        #region IPortraitHandler Members

        public PortraitPersister(Repository.PersistenceService service)
        {
            this.service = service;
        }

        public void Initialize() {}

        public void Start() {}

        public void HandlePortraits(IList<Damany.Imaging.Contracts.Frame> motionFrames, IList<Damany.Imaging.Contracts.Portrait> portraits)
        {
            motionFrames.ToList().ForEach(f =>{ service.SaveFrame(f); service.GetFrame(f.Guid);});
            portraits.ToList().ForEach(p => { service.SavePortrait(p); service.GetPortrait(p.Guid); });
            
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
