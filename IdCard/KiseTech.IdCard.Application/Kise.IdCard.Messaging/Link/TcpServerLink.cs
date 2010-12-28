using System;
using System.Net;
using System.Net.Sockets;

namespace Kise.IdCard.Messaging.Link
{
    public class TcpServerLink : ILink
    {
        private TcpListener _server;
        private TcpClient _client;

        public void Start()
        {
            if (_server == null)
            {
                var local = new IPEndPoint(IPAddress.Loopback, 10000);
                _server = new TcpListener(local);

                _server.Start();

                while (true)
                {
                    var client = _server.AcceptTcpClient();
                    _client = client;
                    while (true)
                    {
                        var reader = new System.IO.StreamReader(_client.GetStream());
                        var msg = reader.ReadLine();

                        var im = new IncomingMessage(msg);
                        RaiseNewMessageReceived(new MiscUtil.EventArgs<IncomingMessage>(im));
                    }
                }

            }
        }

        public void SendAsync(string destination, string message)
        {
            if (_client != null)
            {
                var writer = new System.IO.StreamWriter(_client.GetStream());
                writer.WriteLine(message);
                writer.Flush();
            }
        }

        public event EventHandler<MiscUtil.EventArgs<IncomingMessage>> NewMessageReceived;

        public void RaiseNewMessageReceived(MiscUtil.EventArgs<IncomingMessage> e)
        {
            EventHandler<MiscUtil.EventArgs<IncomingMessage>> handler = NewMessageReceived;
            if (handler != null) handler(this, e);
        }
    }
}