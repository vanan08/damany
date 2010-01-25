using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.Gateways
{
    public class StreamPlayer : GateWayBase<RemoteControlService.IStreamPlayer>
    {
        public StreamPlayer():base(ServiceProxy.ProxyFactory.CreatePlayerProxy){}

        private System.Net.IPAddress lastIp;

        static StreamPlayer instance;

        public static StreamPlayer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StreamPlayer();
                }

                return instance;
            }

        }


        public bool StreamVideo(System.Net.IPAddress ip, string path)
        {
            EnsureProxyCreated(ip);

            if (this.lastIp != null && this.lastIp != ip)
            {
                proxies[this.lastIp].Stop();
            }

            bool result = proxies[ip].StreamVideo(path);

            this.lastIp = ip;

            return result;
            
        }

        public void Stop(System.Net.IPAddress ip)
        {
            
        }

    }
}
