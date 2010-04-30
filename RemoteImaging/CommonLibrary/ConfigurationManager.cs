using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db4objects.Db4o;
using Damany.PC.Domain;

namespace Damany.RemoteImaging.Common
{
    public class ConfigurationManager
    {
        public event EventHandler ConfigurationChanged;

        public void InvokeConfigurationChanged()
        {
            EventHandler handler = ConfigurationChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public static ConfigurationManager GetDefault()
        {
            if (instance == null)
            {
                instance = new ConfigurationManager();
                instance.Initialize();
            }

            return instance;
            
        }

        private void Initialize()
        {
            if (objContainer == null)
            {
                objContainer = Db4oEmbedded.OpenFile("config.db4o");
            }
            
        }

        public IList<CameraInfo> GetCameras()
        {
            return objContainer.Query<CameraInfo>();
        }

        public CameraInfo GetCameraById(int camId)
        {
            var cam = from c in this.GetCameras()
                      where c.Id == camId
                      select c;

            return cam.SingleOrDefault();
        }

        public void AddCamera(CameraInfo camera)
        {
            UpdateCamera(camera);
        }

        public void UpdateCamera(CameraInfo camera)
        {
            this.objContainer.Store(camera);
            this.objContainer.Commit();

            InvokeConfigurationChanged();
        }

        public void DeleteCamera(CameraInfo camera)
        {
            this.objContainer.Delete(camera);
            this.objContainer.Commit();
        }

        public void ClearCameras()
        {
            foreach (var cameraInfo in this.GetCameras())
            {
                this.objContainer.Delete(cameraInfo);
            }
            
        }

        public string GetName(int id)
        {
            var cameras = GetCameras();

            var query = from c in cameras
                        where c.Id == id
                        select c;

            var single = query.SingleOrDefault();

            if (single == null)
            {
                return string.Empty;
            }
            return single.Name;
        }
        private ConfigurationManager() {}

        private  Db4objects.Db4o.IObjectContainer objContainer;

        private static ConfigurationManager instance;
    }
}
