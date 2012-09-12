using System;
using System.Net;
using System.Threading.Tasks;

namespace Kise.IdCard.Messaging.Link
{
    public class FakeLink : ILink
    {
        private Func<string, IncomingMessage> _replyFactory;

        public int DelayInMs { get; set; }

        public FakeLink()
        {
            _replyFactory = i => new IncomingMessage();
        }

        public void Start()
        {

        }


        public Task<IncomingMessage> SendAsync(EndPoint destination, string message)
        {
            var rndDelay = new Random(DateTime.Now.Millisecond);
            TaskEx.Delay(DelayInMs == 0 ? rndDelay.Next(1, 100) : DelayInMs);

            var reply = _replyFactory(message);

            RaiseNewMessageReceived(new MiscUtil.EventArgs<IncomingMessage>(reply));
            throw new NotImplementedException();

        }

        public event EventHandler<MiscUtil.EventArgs<IncomingMessage>> NewMessageReceived;

        public void RaiseNewMessageReceived(MiscUtil.EventArgs<IncomingMessage> e)
        {
            EventHandler<MiscUtil.EventArgs<IncomingMessage>> handler = NewMessageReceived;
            if (handler != null) handler(this, e);
        }


        public void Return(Func<string, IncomingMessage> f)
        {
            _replyFactory = f;
        }

        public char[] GetSplitToken()
        {
            return new[] { '*' };
        }
    }
}