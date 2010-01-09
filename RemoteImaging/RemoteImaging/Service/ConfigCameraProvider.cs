using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Component;

namespace RemoteImaging.Service
{
    public class ConfigCameraProvider : RemoteControlService.IConfigCamera
    {
        SanyoNetCamera sanyoCamera;

        public ConfigCameraProvider(SanyoNetCamera camera)
        {
            if (camera == null)
                throw new ArgumentNullException("camera", "camera is null.");

            this.sanyoCamera = camera;
        }


        #region IConfigCamera Members

        public void SetAGCMode(bool enableAGC, bool enableDigitalGain)
        {
            this.sanyoCamera.SetAgc(enableAGC, enableDigitalGain);
        }


        public void SetShutter(ShutterMode mode, int level)
        {
            this.sanyoCamera.SetShutterSpeed(mode, level);
        }

        public void SetIris(IrisMode mode, int level)
        {
            this.sanyoCamera.SetIrisLevel(mode, level);
        }

        #endregion
    }
}
