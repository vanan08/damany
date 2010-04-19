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
            //if (isDirty)
            {
                camerasCache = objContainer.Query<CameraInfo>();
                var sort = from c in camerasCache
                           orderby c.Id ascending
                           select c;

                camerasCache = sort.ToList();
             
                //isDirty = false;
            }

            return camerasCache;
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

            isDirty = true;

            InvokeConfigurationChanged();
        }

        public void DeleteCamera(CameraInfo camera)
        {
            this.objContainer.Delete(camera);
            this.objContainer.Commit();

            isDirty = true;
        }

        public void ClearCameras()
        {
            foreach (var cameraInfo in objContainer.Query<CameraInfo>())
            {
                this.objContainer.Delete(cameraInfo);
            }

            isDirty = true;

            InvokeConfigurationChanged();
            
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

        private ConfigurationManager()
        {
            isDirty = true;
        }

        private  Db4objects.Db4o.IObjectContainer objContainer;

        private static ConfigurationManager instance;
        private bool isDirty;

        private IList<CameraInfo> camerasCache;
    }
}
