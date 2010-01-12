using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemoteControlService;
using System.ServiceModel;

namespace RemoteImaging.Gateways
{
    public class HostConfig
    {
        static Dictionary<System.Net.IPAddress, IConfigHost> proxies
                    = new Dictionary<System.Net.IPAddress, IConfigHost>();

        private static void EnsureProxyCreated(System.Net.IPAddress ip)
        {
            if (!proxies.ContainsKey(ip))
            {
                var proxy = ServiceProxy.ProxyFactory.CreateConfigHostProxy(ip.ToString());
                proxies.Add(ip, proxy);
            }

            var channel = proxies[ip] as System.ServiceModel.Channels.IChannel;

            if (channel.State == CommunicationState.Faulted)
            {
                proxies[ip] = ServiceProxy.ProxyFactory.CreateConfigHostProxy(ip.ToString());
            }
        }

        public static void SetHostName(System.Net.IPAddress ip, string name)
        {
            EnsureProxyCreated(ip);

            proxies[ip].SetHostName(name);
        }
    }
}
