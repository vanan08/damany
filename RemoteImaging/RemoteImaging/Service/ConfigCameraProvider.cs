using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Component;
using System.ServiceModel;

namespace RemoteImaging.Service
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class ConfigCameraProvider : RemoteControlService.IConfigCamera
    {
        Damany.Component.ICamera camera;

        public ConfigCameraProvider(Damany.Component.ICamera camera)
        {
            if (camera == null)
                throw new ArgumentNullException("camera", "camera is null.");

            this.camera = camera;
        }


        #region IConfigCamera Members

        public void SetAGCMode(bool enableAGC, bool enableDigitalGain)
        {
            this.camera.SetAGCMode(enableAGC, enableDigitalGain);
        }


        public void SetShutter(ShutterMode mode, int level)
        {
            this.camera.SetShutter(mode, level);
        }

        public void SetIris(IrisMode mode, int level)
        {
            this.camera.SetIris(mode, level);
        }

        #endregion
    }
}
