using System;
using System.Threading.Tasks;

namespace Kise.IdCard.Infrastructure.Sms
{
    public interface ILink
    {
        void Start();

        void SendAsync(string destination, string message);
        event EventHandler<MiscUtil.EventArgs<IncomingMessage>> NewMessageReceived;
    }
}