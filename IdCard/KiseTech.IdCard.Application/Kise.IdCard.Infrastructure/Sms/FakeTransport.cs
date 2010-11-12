using System;
using System.Threading.Tasks;

namespace Kise.IdCard.Infrastructure.Sms
{
    public class FakeTransport : ITransport
    {
        private  Func<string, string> _replyMethod;

        public void Start()
        {

        }

        public async Task<string> QueryAsync(string destination, string message)
        {
            var rndDelay = new Random(DateTime.Now.Millisecond);
            await TaskEx.Delay(rndDelay.Next(2000, 5000));

            var reply = _replyMethod(message);
            return reply;

        }

        public void Return(Func<string, string> f)
        {
            _replyMethod = f;
        }

        public char[] GetSplitToken()
        {
            return new[] { '*' };
        }
    }
}