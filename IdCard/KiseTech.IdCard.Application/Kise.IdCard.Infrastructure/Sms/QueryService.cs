using System;
using System.Threading.Tasks;

namespace Kise.IdCard.Infrastructure.Sms
{
    public class QueryService
    {
        private readonly ITransport _transport;

        private int _nextSequenceNumber;

        public QueryService(ITransport transport)
        {
            if (transport == null) throw new ArgumentNullException("transport");
            _transport = transport;
        }

        public void Start()
        {
            _transport.Start();
        }

        public async Task<string> QueryAsync(string destinationNumber, string message)
        {
            var sn = GetNextSequenceNumber();
            var packedMessage = sn.ToString() + GetToken()[0] + message;

            string reply = await _transport.QueryAsync(destinationNumber, packedMessage);

            var tokens = reply.Split(GetToken());
            int echoSn = -1;
            if (int.TryParse(tokens[0], out echoSn))
            {
                if (echoSn == sn)
                {
                    return reply.Substring(tokens[0].Length+1);
                }
            }

            return string.Empty;
        }

        private char[] GetToken()
        {
            return new[] { '*' };
        }


        private int GetNextSequenceNumber()
        {
            var v = _nextSequenceNumber;
            _nextSequenceNumber = ++_nextSequenceNumber % 16;

            return v;
        }
    }
}