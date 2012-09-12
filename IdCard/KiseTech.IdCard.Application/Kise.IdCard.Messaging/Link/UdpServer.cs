using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MiscUtil;

namespace Kise.IdCard.Messaging.Link
{
    public class UdpServer : ILink
    {
        private int _port;
        private Socket _server;
        private System.Threading.Thread _workerThread;
        private static  NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private bool _started;

        public UdpServer(int port)
        {
            _port = port;
        }

        public void Start()
        {
            if (!_started)
            {
                _server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                var ep = new IPEndPoint(IPAddress.Any, _port);
                _server.Bind(ep);

                if (_workerThread == null)
                {
                    _workerThread = new Thread(ReceiveThread);
                    _workerThread.IsBackground = true;
                    _workerThread.Start();
                }

                _started = true;
            }
           
        }

        public Task<IncomingMessage> SendAsync(EndPoint destination, string message)
        {
            if (!_started)
            {
                throw new InvalidOperationException("start is not called");
            }

            var bytes = System.Text.Encoding.Unicode.GetBytes(message);
            _server.SendTo(bytes, destination);
            var tcs = new TaskCompletionSource<IncomingMessage>();
            tcs.SetResult(null);
            return tcs.Task;
        }

        public event EventHandler<EventArgs<IncomingMessage>> NewMessageReceived;

        public void OnNewMessageReceived(EventArgs<IncomingMessage> e)
        {
            EventHandler<EventArgs<IncomingMessage>> handler = NewMessageReceived;
            if (handler != null) handler(this, e);
        }


        private void ReceiveThread()
        {
            while (true)
            {
                var remoteEp = new IPEndPoint(IPAddress.Any, 0);
                EndPoint ep = remoteEp;
                var bytes = new byte[1024];
                var bytesLen = 0;
                try
                {
                    bytesLen = _server.ReceiveFrom(bytes, ref ep);
                }
                catch (Exception)
                {
                    continue;
                }
               

                string byteString = null;
                try
                {
                    byteString = System.Text.Encoding.Unicode.GetString(bytes, 0, bytesLen);
                }
                catch (Exception ex)
                {
                    _logger.ErrorException("数据包格式错误，来自：" + remoteEp, ex );
                    continue;
                }
                
                var msg = new IncomingMessage(byteString);
                msg.Sender = ep;

                try
                {
                    OnNewMessageReceived(new EventArgs<IncomingMessage>(msg)); 
                }
                catch (Exception ex)
                {
                    _logger.ErrorException("数据包处理发生异常", ex);
                }
               
            }
        }
    }
}