using System;
using System.Threading.Tasks;

namespace Kise.IdCard.Infrastructure.Sms
{
    public class FakeTransport : ITransport
    {
        private string[] _queryResults;

        public FakeTransport()
        {
            _queryResults = new string[] { "逃犯", "正常" };
        }

        public void Start()
        {

        }

        public async Task<string> QueryAsync(string destination, string message)
        {
            var rndDelay = new Random(DateTime.Now.Millisecond);
            await TaskEx.Delay(rndDelay.Next(10000, 60000));

            var rndValue = new Random(DateTime.Now.Millisecond);

            var delemiter = GetSplitToken()[0];
            var tokens = message.Split(delemiter);
            var returnMessage = tokens[0] + delemiter + _queryResults[rndValue.Next(_queryResults.Length)];
            return returnMessage;
        }

        public char[] GetSplitToken()
        {
            return new[] { '*' };
        }
    }
}