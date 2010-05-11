using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.PortraitCapturer.DAL;

namespace Damany.Imaging.Handlers
{
    public class PersistenceWriter : Damany.Imaging.Contracts.IPortraitHandler
    {
        private Damany.PortraitCapturer.DAL.PersistenceService service;
        #region IPortraitHandler Members

        public PersistenceWriter(Damany.PortraitCapturer.DAL.PersistenceService service)
        {
            this.service = service;
        }

        public void Initialize() {}

        public void Start() {}

        public void HandlePortraits(IList<Damany.Imaging.Contracts.Frame> motionFrames, IList<Damany.Imaging.Contracts.Portrait> portraits)
        {
            motionFrames.ToList().ForEach(f =>{ service.SaveFrame(f); });
            portraits.ToList().ForEach(p => { service.SavePortrait(p); });
            
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
