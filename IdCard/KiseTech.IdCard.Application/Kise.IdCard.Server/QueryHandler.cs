using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kise.IdCard.Messaging;
using Kise.IdCard.Messaging.Link;
using MiscUtil;

namespace Kise.IdCard.Server
{
    public class QueryHandler
    {
        private readonly ILink _link;
        private Random _random = new Random();
        private readonly IView _view;
        private int idx = 0;

        public event EventHandler<MiscUtil.EventArgs<IncomingMessage>> NewMessageReceived;

        private void InvokeNewMessageReceived(MiscUtil.EventArgs<IncomingMessage> e)
        {
            EventHandler<MiscUtil.EventArgs<IncomingMessage>> handler = NewMessageReceived;
            if (handler != null) handler(this, e);
        }

        public QueryHandler(ILink link, IView view)
        {
            if (link == null) throw new ArgumentNullException("link");
            if (view == null) throw new ArgumentNullException("view");

            _link = link;
            _view = view;
            _link.NewMessageReceived += _link_NewMessageReceived;
        }

        public void Start()
        {
            _link.Start();
        }

        void _link_NewMessageReceived(object sender, MiscUtil.EventArgs<IncomingMessage> e)
        {
            InvokeNewMessageReceived(e);

            var unpackedMsg = string.Empty;
            var sn = -1;
            if (Helper.TryUnpackMessage(e.Value.Message, out sn, out unpackedMsg))
            {
                var reply = Helper.PackMessage(idx.ToString(), sn);
                idx = ++idx % 5;
                System.Threading.Thread.Sleep(_random.Next(10000, 40000));
                _view.AppendText(string.Format("发送应答: " + idx.ToString()));
                _link.SendAsync(e.Value.Sender, reply);
            }
        }
    }
}
