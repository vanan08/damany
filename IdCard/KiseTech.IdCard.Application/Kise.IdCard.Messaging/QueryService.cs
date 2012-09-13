using System;
using System.Collections.Concurrent;
using System.Net;
using System.Threading.Tasks;
using Kise.IdCard.Messaging.Link;

namespace Kise.IdCard.Messaging
{
    public class QueryService
    {

        private object _locker = new object();
        private int _nextSequenceNumber;
        private int _currentSequenceNumber;
        private DateTime _sendQueryTime;

        private ConcurrentDictionary<string, IncomingMessage> _messagesReceived
            = new ConcurrentDictionary<string, IncomingMessage>();


        public int TimeOutInSeconds { get; set; }

        public QueryService(ILink link)
        {

            TimeOutInSeconds = 120;
        }

        public void Start()
        {
            
        }

        public  Task<ReplyMessage> QueryAsync(EndPoint destinationNumber, string message)
        {
            return TaskEx.Run(() =>
                           {
                               var splits = message.Split(new[] {'*'});

                               _sendQueryTime = DateTime.Now;
                               try
                               {
                                   var proxy = new WcfService.IdQueryWcfServiceClient();
                                   var msg = proxy.QueryId(message);
                                   return new ReplyMessage(msg);
                               }
                               catch (Exception ex)
                               {
                                   var reply = new ReplyMessage(string.Empty);
                                   reply.Error = ex;
                                   return reply;
                               }
                           });


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
                                     Message = msg.Message,
                                     ReceiveTime = msg.ReceiveTime,
                                     Sender = msg.Sender
                                 };
                    return rm;
                }
                else if (DateTime.Now - beginTime > TimeSpan.FromSeconds(TimeOutInSeconds))
                {
                    return null;
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
                    Message = e.Value.Message,
                    ReceiveTime = e.Value.ReceiveTime,
                    Sender = e.Value.Sender,
                };

                _messagesReceived.AddOrUpdate(id, msg, (s, i) => msg);
                System.Diagnostics.Debug.WriteLine("add message" + payLoad);
            }

        }

    }
}