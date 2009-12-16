using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.RemoteImaging.Net.Discovery
{
    public class TopicArgs : EventArgs
    {
        Emcaster.Topics.IMessageParser paser;

        public TopicArgs(Emcaster.Topics.IMessageParser parser)
        {
            this.paser = parser;
        }

        public object DataObject
        {
            get
            {
                return paser.ParseObject();
            }
        }

        public string Topic
        {
            get
            {
                return paser.Topic;
            }
        }
    }
}
