using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emcaster.Sockets;
using Emcaster.Topics;

namespace Damany.RemoteImaging.Net.Discovery
{
    class Subscriber
    {

        private MessageParserFactory factory;
        private UdpReceiver receiveSocket;
        private Dictionary<Type, object> handlers;


        public string ListenToIp { get; set; }
        public int ListeToPort { get; set; }

        public void Start()
        {
            factory = new MessageParserFactory();
            MessageParser parser = factory.Create();
            receiveSocket = new UdpReceiver(ListenToIp, ListeToPort);
            receiveSocket.ReceiveEvent += parser.OnBytes;

            receiveSocket.Start();
        }

        public void Subscribe<T>(string topic, Action<T> handler)
        {
            

            TopicSubscriber topicSubscriber = new TopicSubscriber(topic, factory);
            topicSubscriber.Start();

            topicSubscriber.TopicMessageEvent += delegate(IMessageParser msgParser)
            {
               // msgsReceived.Add(msgParser.ParseObject());
            };


        }
    }
}
