using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace RemoteImaging
{
    public class LiveServer
    {
        TcpClient client;
        RealtimeDisplay.Presenter host;
        BinaryFormatter formatter;
        object locker = new object();
        System.Threading.AutoResetEvent go = new System.Threading.AutoResetEvent(false);
        System.Collections.Generic.Queue<System.Drawing.Image> images
            = new System.Collections.Generic.Queue<System.Drawing.Image>();

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


        public void SendLiveImage(System.Drawing.Image img)
        {
            lock (this.locker)
            {
                if (this.images.Count < 2)
                {
                    this.images.Enqueue((System.Drawing.Image) img.Clone());
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
                    

                    System.Drawing.Image img = null;
                    if (images.Count > 0)
                    {
                        lock (this.locker)
                        {
                            img = images.Dequeue();
                        }

                        client.GetStream().ReadByte();
                        
                        formatter.Serialize(client.GetStream(), img);
                        img.Dispose();
                    }
                    else
                        go.WaitOne();
                }
            }
            catch (System.IO.IOException ex)
            {
                this.host.ImageCaptured -= this.host_ImageCaptured;
                return;
            }
                
                
        }
    }
}
