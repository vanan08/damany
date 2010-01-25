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

        bool started;

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
            this.started = true;
        }

        public void Publish(string topic, object data, int timeout)
        {
            if (!this.started) throw new InvalidOperationException("Has not started");

            publisher.PublishObject(topic, data, timeout);
        }



        public bool Started
        {
            get
            {
                return started;
            }
            set
            {
                started = value;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (this.sendSocket != null) this.sendSocket.Dispose();
        }

        #endregion
    }
}
