using System;

namespace Kise.IdCard.Messaging.Link
{
    public interface ILink
    {
        void Start();

        void SendAsync(string destination, string message);
        event EventHandler<MiscUtil.EventArgs<IncomingMessage>> NewMessageReceived;
    }
}