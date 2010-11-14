using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Kise.IdCard.Infrastructure.Sms
{
    public class QueryService
    {
        private readonly ILink _link;

        private object _locker = new object();
        private int _nextSequenceNumber;
        private int _currentSequenceNumber;

        private ConcurrentDictionary<int, IncomingMessage> _messagesReceived
            = new ConcurrentDictionary<int, IncomingMessage>();


        public int TimeOutInSeconds { get; set; }

        public QueryService(ILink link)
        {
            if (link == null) throw new ArgumentNullException("link");

            _link = link;
            _link.NewMessageReceived += _link_NewMessageReceived;

            TimeOutInSeconds = 30;
        }

        public void Start()
        {
            _link.Start();
        }

        public async Task<ReplyMessage> QueryAsync(string destinationNumber, string message)
        {
            string packedMessage;
            int sn = PackMessage(message, out packedMessage);

            _link.SendAsync(destinationNumber, packedMessage);

            var reply = await GetReplyMessage(sn);
            return reply;
        }

        private async Task<ReplyMessage> GetReplyMessage(int sn)
        {
            var beginTime = DateTime.Now;
            while (true)
            {
                await TaskEx.Delay(3000);
                var msg = FindReply(sn);
                if (msg != null && msg.ReceiveTime > beginTime)
                {
                    var rm = new ReplyMessage()
                                 {
                                     IsTimedOut = false,
                                     Message = msg.Message,
                                     ReceiveTime = msg.ReceiveTime,
                                     Sender = msg.Sender
                                 };
                    return rm;
                }
                else if (DateTime.Now - beginTime > TimeSpan.FromSeconds(TimeOutInSeconds))
                {
                    return ReplyMessage.TimeOut;
                }

            }
        }

        private IncomingMessage FindReply(int sn)
        {
            IncomingMessage msg = null;
            if (_messagesReceived.ContainsKey(sn))
            {
                _messagesReceived.TryRemove(sn, out msg);
            }

            return msg;
        }

        private int PackMessage(string message, out string packedMessage)
        {
            var sn = GetNextSequenceNumber();
            packedMessage = sn.ToString() + GetToken() + message;
            return sn;
        }

        private char GetToken()
        {
            return '*';
        }


        private int GetNextSequenceNumber()
        {
            var v = _nextSequenceNumber;

            lock (_locker)
            {
                _nextSequenceNumber = ++_nextSequenceNumber % 16;
            }

            return v;
        }


        void _link_NewMessageReceived(object sender, MiscUtil.EventArgs<IncomingMessage> e)
        {
            int sequenceNumber = -1;
            string payload = null;
            if (TryUnpackMessage(e.Value.Message, out sequenceNumber, out payload))
            {
                var msg = new IncomingMessage()
                {
                    Message = payload,
                    ReceiveTime = e.Value.ReceiveTime,
                    Sender = e.Value.Sender,
                };

                _messagesReceived.AddOrUpdate(sequenceNumber, msg, (s, i) => msg);
            }

        }

        private bool TryUnpackMessage(string message, out int sequenceNumber, out string payload)
        {
            if (message == null) throw new ArgumentNullException("message");

            sequenceNumber = -1;
            payload = null;

            var tokens = message.Split(GetToken());
            if (tokens.Length >= 2)
            {
                if (int.TryParse(tokens[0], out sequenceNumber))
                {
                    payload = string.Join(new string(new char[] { GetToken() }), tokens, 1, tokens.Length - 1);
                    return true;
                }
            }

            return false;
        }

    }
}