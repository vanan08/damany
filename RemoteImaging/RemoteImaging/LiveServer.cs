using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using Damany.RemoteImaging.Common;

namespace RemoteImaging
{
    public class LiveServer
    {
        TcpClient client;
        RealtimeDisplay.Presenter host;
        BinaryFormatter formatter;
        object locker = new object();
        System.Threading.AutoResetEvent go = new System.Threading.AutoResetEvent(false);
        System.Collections.Generic.Queue<Image> images
            = new System.Collections.Generic.Queue<Image>();

        public LiveServer(TcpClient client, RealtimeDisplay.Presenter host)
        {
            this.client = client;
            this.host = host;
            host.ImageCaptured += new EventHandler<ImageCapturedEventArgs>(host_ImageCaptured);
            formatter = new BinaryFormatter();
        }

        void host_ImageCaptured(object sender, ImageCapturedEventArgs e)
        {
            this.SendLiveImage(e.ImageCaptured);
        }


        public void SendLiveImage(Image img)
        {
            lock (this.locker)
            {
                if (this.images.Count < 2)
                {
                    this.images.Enqueue((Image)img.Clone());
                    this.go.Set();
                }

            }
        }

        public void Start()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(this.DoSend);

        }


        public void DoSend(object state)
        {
            try
            {
                while (true)
                {
                    if (images.Count > 0)
                    {
                        //wait for the client to be ready
                        client.GetStream().ReadByte();

                        Image img = null;
                        lock (this.locker)
                        {
                            img = images.Dequeue();
                        }

                        Frame frame = new Frame();
                        frame.image = img;
                        frame.timeStamp = DateTime.Now;

                        formatter.Serialize(client.GetStream(), frame);
                        img.Dispose();
                    }
                    else
                        go.WaitOne();
                }
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                this.host.ImageCaptured -= this.host_ImageCaptured;
                return;
            }
                
                
        }
    }
}
