using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

namespace Kise.IdCard.Messaging.Link
{
    public class TcpClientLink : ILink
    {
        private System.Net.Sockets.TcpClient _tcpClient;
        private System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _formatter
            = new BinaryFormatter();

        public int PortToConnect { get; set; }

        public async void Start()
        {
            if (PortToConnect == 0)
            {
                throw new InvalidOperationException("PortToConnect Must be set");
            }

            if (_tcpClient == null)
            {
                _tcpClient = new System.Net.Sockets.TcpClient();
                await _tcpClient.ConnectAsync(IPAddress.Loopback, PortToConnect);

                while (true)
                {
                    var msg = _formatter.Deserialize(_tcpClient.GetStream());
                    var im = new IncomingMessage(msg as string);

                    InvokeNewMessageReceived(new MiscUtil.EventArgs<IncomingMessage>(im));
                }
            }
        }

        public void SendAsync(string destination, string message)
        {
            if (_tcpClient == null) throw new InvalidOperationException("Start must be called first");

            _formatter.Serialize(_tcpClient.GetStream(), message);
            _tcpClient.GetStream().Flush();
        }

        public event EventHandler<MiscUtil.EventArgs<IncomingMessage>> NewMessageReceived;

        public void InvokeNewMessageReceived(MiscUtil.EventArgs<IncomingMessage> e)
        {
            EventHandler<MiscUtil.EventArgs<IncomingMessage>> handler = NewMessageReceived;
            if (handler != null) handler(this, e);
        }
    }
}