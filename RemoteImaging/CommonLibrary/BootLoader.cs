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
        public Damany.PortraitCapturer.Repository.PersistenceService repository;

        public void Load()
        {
            ConfigurationManager config = ConfigurationManager.GetDefault();

            ReportProgress(0, "初始化数据库");
            SearchLineBuilder.GetDefaultPersistenceService();
            this.repository = SearchLineBuilder.GetDefaultPersistenceService();
            ReportProgress(50, "数据库初始化成功");

            foreach (var cam in config.GetCameras())
            {
                string url = @"D:\20090505";
                ReportProgress(50, "初始化摄像头: " + cam.Location.ToString());
                var controller = SearchLineBuilder.BuildNewSearchLine(cam);
                this.controllers.Add(controller);
                ReportProgress(90, "初始化摄像头: " + cam.Location.ToString() + "成功");

            }

        }
    }
}
