using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemoteControlService;
using System.ServiceModel;

namespace RemoteImaging.Gateways
{
    public static class CameraConfig
    {
        static Dictionary<System.Net.IPAddress, IConfigCamera> proxies
            = new Dictionary<System.Net.IPAddress, IConfigCamera>();

        private static void EnsureProxyCreated(System.Net.IPAddress ip)
        {
            if (!proxies.ContainsKey(ip))
            {
                var proxy = ServiceProxy.ProxyFactory.CreateConfigCameraProxy(ip.ToString());
                proxies.Add(ip, proxy);
            }

            var channel = proxies[ip] as System.ServiceModel.Channels.IChannel;

            if (channel.State == CommunicationState.Faulted)
            {
                proxies[ip] = ServiceProxy.ProxyFactory.CreateConfigCameraProxy(ip.ToString());
            }
        }


        public static void SetIris(System.Net.IPAddress ip, Damany.Component.IrisMode mode, int level)
        {
            EnsureProxyCreated(ip);

            proxies[ip].SetIris(mode, level);

        }

        public static void SetShutter(System.Net.IPAddress ip, Damany.Component.ShutterMode mode, int level)
        {
            EnsureProxyCreated(ip);

            proxies[ip].SetShutter(mode, level);

        }

        public static void SetAgc(System.Net.IPAddress ip, bool agcEnable, bool digitalGainEnable)
        {
            EnsureProxyCreated(ip);

            proxies[ip].SetAGCMode(agcEnable, digitalGainEnable);

        }
    }
}
