using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using RemoteControlService;
using System.Xml;

namespace RemoteImaging.ServiceProxy
{
    public class ProxyFactory
    {

        public static TInterface CreateProxy<TInterface>(string uri)
        {
            EndpointAddress ep = new EndpointAddress(uri);

            NetTcpBinding tcpBinding = BindingFactory.CreateNetTcpBinding();

            TInterface proxy = ChannelFactory<TInterface>.CreateChannel(tcpBinding, ep);
            return proxy;
        }

    }
}
