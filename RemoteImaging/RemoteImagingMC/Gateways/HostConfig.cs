using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemoteControlService;
using System.ServiceModel;

namespace RemoteImaging.Gateways
{
    public class HostConfig : GateWayBase<IConfigHost>
    {
        public HostConfig():base(ServiceProxy.ProxyFactory.CreateConfigHostProxy) {}

        static HostConfig instance;

        public static HostConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HostConfig();
                }

                return instance;
            }

        }


        public void SetHostName(System.Net.IPAddress ip, string name)
        {
            EnsureProxyCreated(ip);

            proxies[ip].SetHostName(name);
        }
    }
}
