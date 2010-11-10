using System;

namespace Kise.IdCard.Infrastructure.Sms
{
    public interface ISmsService
    {
        void Send(string destinationNumber, string message,
                    Action<bool> deliverCallback, Action<string> responseCallback);
    }
}