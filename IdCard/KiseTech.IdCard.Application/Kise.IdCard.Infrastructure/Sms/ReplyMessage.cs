using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kise.IdCard.Infrastructure.Sms
{
    public class ReplyMessage
    {
        public static ReplyMessage TimeOut;

        static ReplyMessage()
        {
            TimeOut = new ReplyMessage();
        }

        public ReplyMessage()
        {
            IsTimedOut = true;
            Message = string.Empty;
            Sender = string.Empty;
        }

        public ReplyMessage(string message)
            : this()
        {
            IsTimedOut = false;
            Message = message;
        }

        public DateTime ReceiveTime { get; set; }
        public string Message { get; set; }
        public string Sender { get; set; }
        public bool IsTimedOut { get; set; }
    }


}
