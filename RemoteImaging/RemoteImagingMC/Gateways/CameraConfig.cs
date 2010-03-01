using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemoteControlService;
using System.ServiceModel;

namespace RemoteImaging.Gateways
{
    public class CameraConfig : GateWayBase<IConfigCamera>
    {

        public CameraConfig() : base(ServiceProxy.ProxyFactory.CreateConfigCameraProxy) {}

        static CameraConfig instance;

        public static CameraConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CameraConfig();
                }

                return instance;
            }

        }


        public void SetIris(System.Net.IPAddress ip, int cameraId, Damany.Component.IrisMode mode, int level)
        {
            EnsureProxyCreated(ip);

            proxies[ip].SetIris(cameraId, mode, level);

        }

        public void SetShutter(System.Net.IPAddress ip, int cameraId, Damany.Component.ShutterMode mode, int level)
        {
            EnsureProxyCreated(ip);

            proxies[ip].SetShutter(cameraId, mode, level);

        }

        public void SetAgc(System.Net.IPAddress ip, int cameraId, bool agcEnable, bool digitalGainEnable)
        {
            EnsureProxyCreated(ip);

            proxies[ip].SetAGCMode(cameraId, agcEnable, digitalGainEnable);

        }

    }
}
