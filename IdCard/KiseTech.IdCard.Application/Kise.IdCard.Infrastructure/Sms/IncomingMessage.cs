using System;

namespace Kise.IdCard.Infrastructure.Sms
{
    public class IncomingMessage
    {
        public IncomingMessage(string msg) : this()
        {
            Message = msg;
            Sender = string.Empty;
        }


        public IncomingMessage()
        {
            ReceiveTime = DateTime.Now;
        }

        public DateTime ReceiveTime { get; set; }

        public string Sender { get; set; }
        public string Message { get; set; }
    }
}
