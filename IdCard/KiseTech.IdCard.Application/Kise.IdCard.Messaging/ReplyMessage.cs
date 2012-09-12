using System;
using System.Net;

namespace Kise.IdCard.Messaging
{
    public class ReplyMessage
    {

        public ReplyMessage()
        {
            Message = string.Empty;
        }

        public ReplyMessage(string message)
            : this()
        {
            Message = message;
        }

        public DateTime ReceiveTime { get; set; }
        public string Message { get; set; }
        public EndPoint Sender { get; set; }

        public Exception Error { get; set; }
    }


}
