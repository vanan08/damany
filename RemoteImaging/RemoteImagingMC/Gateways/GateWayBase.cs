using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace RemoteImaging.Gateways
{
    public class GateWayBase<TIProxy>
    {
        protected Dictionary<System.Net.IPAddress, TIProxy> proxies
                    = new Dictionary<System.Net.IPAddress, TIProxy>();

        protected CreateProxy<TIProxy> creator;

        public GateWayBase(CreateProxy<TIProxy> creator)
        {
            this.creator = creator;
        }

        protected void EnsureProxyCreated(System.Net.IPAddress ip)
        {
            if (!proxies.ContainsKey(ip))
            {
                var proxy = this.creator.Invoke(ip);
                proxies.Add(ip, proxy);
            }

            var channel = proxies[ip] as System.ServiceModel.Channels.IChannel;

            if (channel.State == CommunicationState.Faulted)
            {
                proxies[ip] = this.creator.Invoke(ip);
            }
        }

    }
}
