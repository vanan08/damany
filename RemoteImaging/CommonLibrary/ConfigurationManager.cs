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

        public void AddCamera(CameraInfo camera)
        {
            UpdateCamera(camera);
        }

        public void UpdateCamera(CameraInfo camera)
        {
            this.objContainer.Store(camera);
            this.objContainer.Commit();
        }

        public void DeleteCamera(CameraInfo camera)
        {
            this.objContainer.Delete(camera);
            this.objContainer.Commit();
        }

        private ConfigurationManager() {}

        private  Db4objects.Db4o.IObjectContainer objContainer;

        private static ConfigurationManager instance;
    }
}
