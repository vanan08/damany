using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.RemoteImaging.Net.Discovery
{
    public class UdpBus : IDisposable
    {
        Publisher pub;
        Subscriber sub;

        string ip;
        int port;

        public UdpBus(string ip, int port)
        {
            this.ip = ip;
            this.port = port;

            pub = new Publisher(ip, port);
            sub = new Subscriber(ip, port);
        }

        public void Start()
        {
            this.pub.Start();
            this.sub.Start();
        }

        public void Publish(string topic, object data, int timeout)
        {
            if (!pub.Started) throw new InvalidOperationException("Has not started");

            this.pub.Publish(topic, data, timeout);
        }

        public void Subscrib(string topic, EventHandler<TopicArgs> handler)
        {
            this.sub.Subscribe(topic, handler);
        }


        #region IDisposable Members

        public void Dispose()
        {
            if (this.sub != null)
            {
                this.sub.Dispose();
            }

            if (this.pub != null)
            {
                this.pub.Dispose();
            }
        }

        #endregion
    }
}
