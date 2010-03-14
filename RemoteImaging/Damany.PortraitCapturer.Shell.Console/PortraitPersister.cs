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
        private Repository.PersistenceService repository;
        private const string root_dir = @".\Data";
        private const string image_dir = @".\Data\Images";
        #region IPortraitHandler Members

        public void Initialize()
        {
            System.IO.Directory.CreateDirectory(root_dir);
            System.IO.Directory.CreateDirectory(image_dir);

            var store = new Db4oProvider(System.IO.Path.Combine(root_dir, "images.db4o"));

            repository = new Damany.PortraitCapturer.Repository.PersistenceService(
                                    store, GuidToPath, GuidToPath);
        }

        public void Start()
        {
            
        }

        public void HandlePortraits(IList<Damany.Imaging.Contracts.Frame> motionFrames, IList<Damany.Imaging.Contracts.Portrait> portraits)
        {
            motionFrames.ToList().ForEach(f =>{ repository.SaveFrame(f); repository.GetFrame(f.Guid);});
            portraits.ToList().ForEach(p => { repository.SavePortrait(p); repository.GetPortrait(p.Guid); });
            
        }

        public void Stop()
        {
            
        }

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

        private string GuidToPath(Damany.Imaging.Contracts.CapturedObject obj)
        {
            var path = System.IO.Path.Combine(image_dir, obj.Guid.ToString() + ".jpg");
            return path;
        }
    }
}
