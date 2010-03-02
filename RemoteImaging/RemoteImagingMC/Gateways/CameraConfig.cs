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


        public void SetIris(System.Net.IPAddress ip, Damany.Component.IrisMode mode, int level)
        {
            EnsureProxyCreated(ip);

            proxies[ip].SetIris(mode, level);

        }

        public void SetShutter(System.Net.IPAddress ip, Damany.Component.ShutterMode mode, int level)
        {
            EnsureProxyCreated(ip);

            proxies[ip].SetShutter(mode, level);

        }

        public void SetAgc(System.Net.IPAddress ip, bool agcEnable, bool digitalGainEnable)
        {
            EnsureProxyCreated(ip);

            proxies[ip].SetAGCMode(agcEnable, digitalGainEnable);

        }

    }
}
