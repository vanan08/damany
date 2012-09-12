using System;
using System.Net;

namespace Kise.IdCard.Messaging
{
    public class IncomingMessage
    {
        public IncomingMessage(string msg) : this()
        {
            Message = msg;
            Sender = null;
        }


        public IncomingMessage()
        {
            ReceiveTime = DateTime.Now;
        }

        public DateTime ReceiveTime { get; set; }

        public EndPoint Sender { get; set; }
        public string Message { get; set; }
    }
}
