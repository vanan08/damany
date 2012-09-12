using System;
using System.Net;
using System.Threading.Tasks;

namespace Kise.IdCard.Messaging.Link
{
    public interface ILink
    {
        void Start();

        Task<IncomingMessage> SendAsync(EndPoint destination, string message);
        event EventHandler<MiscUtil.EventArgs<IncomingMessage>> NewMessageReceived;
    }
}