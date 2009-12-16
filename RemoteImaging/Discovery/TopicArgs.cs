using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.RemoteImaging.Net.Discovery
{
    public class TopicArgs : EventArgs
    {
        Emcaster.Topics.IMessageParser parser;

        public TopicArgs(Emcaster.Topics.IMessageParser parser)
        {
            this.parser = parser;
        }

        public object DataObject
        {
            get
            {
                return parser.ParseObject();
            }
        }

        public string Topic
        {
            get
            {
                return parser.Topic;
            }
        }

        public System.Net.IPEndPoint From
        {
            get
            {
                return parser.EndPoint as System.Net.IPEndPoint;
            }
        }
    }
}
