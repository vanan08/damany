using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kise.IdCard.Messaging;
using Kise.IdCard.Messaging.Link;

namespace Kise.IdCard.Server
{
    public class QueryHandler
    {
        private readonly ILink _link;

        public QueryHandler(ILink link)
        {
            if (link == null) throw new ArgumentNullException("link");

            _link = link;
            _link.NewMessageReceived += _link_NewMessageReceived;
        }

        public void Start()
        {
            _link.Start();
        }

        void _link_NewMessageReceived(object sender, MiscUtil.EventArgs<IncomingMessage> e)
        {
            var unpackedMsg = string.Empty;
            var sn = -1;
            if (Helper.TryUnpackMessage(e.Value.Message, out sn, out unpackedMsg))
            {
                var status = DateTime.Now.Millisecond % 4;
                var reply = Helper.PackMessage(status.ToString(), sn);
                _link.SendAsync(e.Value.Sender, reply);
            }
        }
    }
}
