using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MiscUtil;

namespace Kise.IdCard.Messaging.Link
{
    public class UdpClient : ILink
    {
        private IPEndPoint _server;
        private Timer _timer;

        public UdpClient(IPEndPoint server)
        {
            if (server == null) throw new ArgumentNullException("server");
            _server = server;
        }

        public void Start()
        {
            
        }

        public Task<IncomingMessage> SendAsync(EndPoint destination, string message)
        {
            if (message == null) throw new ArgumentNullException("message");

            var msgBytes = System.Text.Encoding.Unicode.GetBytes(message);
            var tcs = new TaskCompletionSource<IncomingMessage>();
            var udp = new System.Net.Sockets.UdpClient();

            try
            {
                udp.Send(msgBytes, msgBytes.Length, _server);
            }
            catch (Exception ex)
            {
               tcs.SetException(ex);
                udp.Close();
            }
            
            AsyncCallback callback = delegate(IAsyncResult ar)
                                         {
                                             var udp1 = (System.Net.Sockets.UdpClient) ar.AsyncState;
                                             var rempteEp = new IPEndPoint(IPAddress.Any, 0);
                                             try
                                             {
                                                 var replyBytes = udp1.EndReceive(ar, ref rempteEp);
                                                 var reply = System.Text.Encoding.Unicode.GetString(replyBytes);
                                                 var incomingMessage = new IncomingMessage(reply);
                                                 tcs.TrySetResult(incomingMessage);
                                                 udp.Close();
                                             }
                                             catch (Exception ex)
                                             {
                                                 tcs.TrySetException(ex);
                                                 udp.Close();
                                             }
                                         };

            udp.BeginReceive(callback, udp);
            _timer = new Timer(state =>
                                   {
                                       tcs.TrySetCanceled();
                                       udp.Close();
                                   }, null, TimeSpan.FromSeconds(10), TimeSpan.FromMilliseconds(-1));
            
            
            
            return tcs.Task;

        }

       


        public event EventHandler<EventArgs<IncomingMessage>> NewMessageReceived;

        public void OnNewMessageReceived(EventArgs<IncomingMessage> e)
        {
            EventHandler<EventArgs<IncomingMessage>> handler = NewMessageReceived;
            if (handler != null) handler(this, e);
        }
    }
}