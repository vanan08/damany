using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emcaster.Sockets;
using Emcaster.Topics;

namespace Damany.RemoteImaging.Net.Discovery
{
    public class Publisher : IDisposable
    {
        UdpSource sendSocket;
        BatchWriter writer;
        TopicPublisher publisher;

        public Publisher(string announceIp, int port)
        {
            this.AnnounceIp = announceIp;
            this.AnnouncePort = port;
            sendSocket = new UdpSource(announceIp, port);
            writer = new BatchWriter(sendSocket, 1024*128);
            publisher = new TopicPublisher(writer);
        }

        public string AnnounceIp { get; set; }

        public int AnnouncePort {get;set;}

        public void Start()
        {
            sendSocket.Start();
            publisher.Start();
        }

        public void Publish(string topic, object data, int timeout)
        {
            publisher.PublishObject(topic, data, timeout);
        }



        #region IDisposable Members

        public void Dispose()
        {
            if (this.sendSocket != null) this.sendSocket.Dispose();
        }

        #endregion
    }
}
