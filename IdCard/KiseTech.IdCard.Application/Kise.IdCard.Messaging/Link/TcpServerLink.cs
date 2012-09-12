using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Kise.IdCard.Messaging.Link
{
    public class TcpServerLink : ILink
    {
        private TcpListener _server;
        private TcpClient _client;
        private BinaryFormatter _formatter;

        public TcpServerLink(int port)
        {
            Port = port;
            _formatter = new BinaryFormatter();
        }

        public int Port { get; set; }

        public void Start()
        {
            if (Port == 0)
            {
                throw new InvalidOperationException("Port must be set");
            }

            if (_server == null)
            {
                var local = new IPEndPoint(IPAddress.Loopback, Port);
                _server = new TcpListener(local);

                _server.Start();

                while (true)
                {
                    var client = _server.AcceptTcpClient();
                    _client = client;
                    var bs = new BinaryFormatter();
                    while (true)
                    {
                        try
                        {
                            var obj = bs.Deserialize(_client.GetStream());
                            var im = new IncomingMessage(obj as string);
                            RaiseNewMessageReceived(new MiscUtil.EventArgs<IncomingMessage>(im));
                        }
                        catch (Exception ex)
                        {
                            if (ex is SocketException && (ex as SocketException).SocketErrorCode == SocketError.ConnectionReset)
                            {
                                break;
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                }

            }
        }


        public Task<IncomingMessage> SendAsync(EndPoint destination, string message)
        {
            if (_client != null)
            {
                _formatter.Serialize(_client.GetStream(), message);
                _client.GetStream().Flush();
            }
            
            throw new NotImplementedException();
        }

        public event EventHandler<MiscUtil.EventArgs<IncomingMessage>> NewMessageReceived;

        public void RaiseNewMessageReceived(MiscUtil.EventArgs<IncomingMessage> e)
        {
            EventHandler<MiscUtil.EventArgs<IncomingMessage>> handler = NewMessageReceived;
            if (handler != null) handler(this, e);
        }
    }
}