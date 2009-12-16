using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emcaster.Sockets;
using Emcaster.Topics;

namespace Damany.RemoteImaging.Net.Discovery
{
    public class Subscriber : IDisposable
    {

        private MessageParser parser;
        private MessageParserFactory factory;
        private UdpReceiver receiveSocket;


        public string ListenToIp { get; set; }
        public int ListeToPort { get; set; }

        public Subscriber(string listenToIp, int listenToPort)
        {
            this.ListenToIp = listenToIp;
            this.ListeToPort = listenToPort;

            factory = new MessageParserFactory();
            parser = factory.Create();
        }

        public void Start()
        {
            receiveSocket = new UdpReceiver(ListenToIp, ListeToPort);
            receiveSocket.ReceiveEvent += parser.OnBytes;

            receiveSocket.Start();
        }

        public void Subscribe(string topic, EventHandler<TopicArgs> handler)
        {
            TopicSubscriber topicSubscriber = new TopicSubscriber(topic, factory);
            topicSubscriber.Start();

            topicSubscriber.TopicMessageEvent += delegate(IMessageParser msgParser)
            {
                handler(msgParser.EndPoint, new TopicArgs(msgParser) );
            };
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (this.receiveSocket != null) this.receiveSocket.Dispose();
        }

        #endregion
    }
}
