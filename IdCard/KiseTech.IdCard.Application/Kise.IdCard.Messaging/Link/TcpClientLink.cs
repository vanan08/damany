using System;
using System.IO;
using System.Net;

namespace Kise.IdCard.Messaging.Link
{
    public class TcpClientLink : ILink
    {
        private System.Net.Sockets.TcpClient _tcpClient;
        private System.IO.StreamWriter _writer;

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

                var reader = new System.IO.StreamReader(_tcpClient.GetStream());
                _writer = new StreamWriter(_tcpClient.GetStream());
                while (true)
                {
                    var msg = await reader.ReadLineAsync();
                    var im = new IncomingMessage(msg);

                    InvokeNewMessageReceived(new MiscUtil.EventArgs<IncomingMessage>(im));
                }
            }
        }

        public void SendAsync(string destination, string message)
        {
            _writer.WriteLine(message);
            _writer.Flush();
        }

        public event EventHandler<MiscUtil.EventArgs<IncomingMessage>> NewMessageReceived;

        public void InvokeNewMessageReceived(MiscUtil.EventArgs<IncomingMessage> e)
        {
            EventHandler<MiscUtil.EventArgs<IncomingMessage>> handler = NewMessageReceived;
            if (handler != null) handler(this, e);
        }
    }
}