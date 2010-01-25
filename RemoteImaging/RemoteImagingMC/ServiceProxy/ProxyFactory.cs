using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using RemoteControlService;
using System.Xml;

namespace RemoteImaging.ServiceProxy
{
    public static class ProxyFactory
    {
        static int searchPort = 8000;
        static int configHostPort = 8001;
        static int configCameraPort = 8002;
        static int playerPort = 4567;


        public static ISearch CreateSearchProxy(System.Net.IPAddress ip)
        {
            return CreateProxy<ISearch>(ip, searchPort);
        }

        public static IStreamPlayer CreatePlayerProxy(System.Net.IPAddress ip)
        {
            return CreateProxy<IStreamPlayer>(ip, playerPort);
        }

        public static IConfigHost CreateConfigHostProxy(System.Net.IPAddress ip)
        {
            return CreateProxy<IConfigHost>(ip, configHostPort);
        }

        public static IConfigCamera CreateConfigCameraProxy(System.Net.IPAddress ip)
        {
            return CreateProxy<IConfigCamera>(ip, configCameraPort);
        }

        public static TInterface CreateProxy<TInterface>(System.Net.IPAddress ip, int port)
        {
            string address = string.Format("net.tcp://{0}:{1}/TcpService", ip, port);
            return CreateProxy<TInterface>(address);
        }

        private static TInterface CreateProxy<TInterface>(string uri)
        {
            EndpointAddress ep = new EndpointAddress(uri);

            NetTcpBinding tcpBinding = BindingFactory.CreateNetTcpBinding();

            return ChannelFactory<TInterface>.CreateChannel(tcpBinding, ep);
        }

    }
}
