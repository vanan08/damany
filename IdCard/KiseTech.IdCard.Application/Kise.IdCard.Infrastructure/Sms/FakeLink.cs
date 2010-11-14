using System;
using System.Threading.Tasks;

namespace Kise.IdCard.Infrastructure.Sms
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


        public async void SendAsync(string destination, string message)
        {
            var rndDelay = new Random(DateTime.Now.Millisecond);
            await TaskEx.Delay(DelayInMs == 0 ? rndDelay.Next(1, 100) : DelayInMs);

            var reply = _replyFactory(message);

            RaiseNewMessageReceived(new MiscUtil.EventArgs<IncomingMessage>(reply));

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