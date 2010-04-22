using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.RemoteImaging.Common
{
    public class BootLoader
    {
        public Action<int, object> ReportProgress { get; set; }
        public IList<Damany.Imaging.Processors.FaceSearchController> controllers
            = new List<Damany.Imaging.Processors.FaceSearchController>();
        public Damany.PortraitCapturer.DAL.Providers.LocalDb4oProvider repository;

        public BootLoader()
        {
            this.ReportProgress = delegate { };
        }

        public void Load(string databaseRoot)
        {
           
        }
    }
}
