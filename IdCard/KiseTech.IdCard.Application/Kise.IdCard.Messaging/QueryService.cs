using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Kise.IdCard.Messaging.Link;

namespace Kise.IdCard.Messaging
{
    public class QueryService
    {
        private readonly ILink _link;

        private object _locker = new object();
        private int _nextSequenceNumber;
        private int _currentSequenceNumber;
        private DateTime _sendQueryTime;

        private ConcurrentDictionary<string, IncomingMessage> _messagesReceived
            = new ConcurrentDictionary<string, IncomingMessage>();


        public int TimeOutInSeconds { get; set; }

        public QueryService(ILink link)
        {
            if (link == null) throw new ArgumentNullException("link");

            _link = link;
            _link.NewMessageReceived += _link_NewMessageReceived;

            TimeOutInSeconds = 120;
        }

        public void Start()
        {
            System.Threading.Tasks.TaskEx.Run(()=>_link.Start());
        }

        public async Task<ReplyMessage> QueryAsync(string destinationNumber, string message)
        {
            //var sn = GetNextSequenceNumber();
            ////string packedMessage = Helper.PackMessage(message, sn);

            var splits = message.Split(new[] { '*' });

            _sendQueryTime = DateTime.Now;
            _link.SendAsync(destinationNumber, message);

            ReplyMessage reply = await GetReplyMessage(splits[0]);
            return reply;
        }

        private async Task<ReplyMessage> GetReplyMessage(string id)
        {
            var beginTime = DateTime.Now;
            while (true)
            {
                await TaskEx.Delay(3000);
                var msg = FindReply(id);
                System.Diagnostics.Debug.WriteLine("try receive message: ");
                if (msg != null)
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

        private IncomingMessage FindReply(string id)
        {
            IncomingMessage msg = null;
            if (_messagesReceived.ContainsKey(id))
            {
                _messagesReceived.TryRemove(id, out msg);
                System.Diagnostics.Debug.WriteLine("remove message");
            }

            return msg;
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
            var starIdx = e.Value.Message.IndexOf('*');
            var id = e.Value.Message.Substring(0, starIdx);
            var payLoad = e.Value.Message.Substring(starIdx + 1);
            //if (Helper.TryUnpackMessage(e.Value.Message, out sequenceNumber, out payload))
            {
                var msg = new IncomingMessage()
                {
                    Message = payLoad,
                    ReceiveTime = e.Value.ReceiveTime,
                    Sender = e.Value.Sender,
                };

                _messagesReceived.AddOrUpdate(id, msg, (s, i) => msg);
                System.Diagnostics.Debug.WriteLine("add message" + payLoad);
            }

        }

    }
}