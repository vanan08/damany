using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Sockets;
using System.Drawing;
using System.Threading;
using Damany.RemoteImaging.Common;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace RemoteImaging.RealtimeDisplay
{
    class LiveClient
    {
        TcpClient client;
        BinaryFormatter formatter;
        Host peer;

        public event EventHandler<ImageCapturedEventArgs> ImageReceived;
        public event EventHandler ConnectAborted;
        SynchronizationContext context;


        public object Tag { get; set; }

        public LiveClient(TcpClient client, Host peer)
        {
            context = SynchronizationContext.Current;
            this.peer = peer;

            this.client = client;
            formatter = new BinaryFormatter();
        }


        void FireImageReceivedEvent(Frame frame)
        {
            if (this.ImageReceived != null)
            {
                ImageCapturedEventArgs args = new ImageCapturedEventArgs
                {
                    FrameCaptured = frame,
                };

                this.ImageReceived(this, args);
            }
        }


        public void Start()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(DoReceiveImage);
        }

        public void Stop()
        {
            exit = true;
        }

        bool exit = false;

        private void OnConnectAborted()
        {
            if (this.ConnectAborted != null)
            {
                this.ConnectAborted(this, EventArgs.Empty);
            }
        }

        void DoReceiveImage(object state)
        {
            try
            {
                while (!exit)
                {
                    client.GetStream().WriteByte(0);
                    client.GetStream().Flush();
                    Frame frame = (Frame)formatter.Deserialize(client.GetStream());
                    this.FireImageReceivedEvent(frame);
                }

            }
            catch(Exception ex)
            {
                bool reThrow = ExceptionPolicy.HandleException(ex, Constants.ExceptionPolicyLogging);

                context.Post(o => OnConnectAborted(), null);
            }
            finally
            {
                client.Close();
            }
        }
    }
}
