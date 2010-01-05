using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Sockets;
using System.Drawing;
using System.Threading;
using Damany.RemoteImaging.Common;

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
        Size imgSize = Size.Empty;

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
                    imgSize = frame.image.Size;
                    this.FireImageReceivedEvent(frame);
                }

            }
            catch
            {
                Bitmap bmp = new Bitmap(imgSize.Width, imgSize.Height);
                int fontSize = imgSize.Height / 10;

                using (Graphics g = Graphics.FromImage(bmp))
                using (Font font = new Font(FontFamily.GenericSansSerif, fontSize, GraphicsUnit.Pixel))
                {
                    g.FillRectangle(Brushes.Black, new Rectangle(0, 0, imgSize.Width, imgSize.Height));
                    StringFormat fmt = new StringFormat();
                    fmt.Alignment = StringAlignment.Center;
                    fmt.LineAlignment = StringAlignment.Center;
                    g.DrawString("连接错误",
                        font,
                        Brushes.White,
                        new RectangleF(0, 0, imgSize.Width, imgSize.Height),
                        fmt
                        );

                    Frame f = new Frame();
                    f.image = bmp;
                    f.timeStamp = DateTime.Now;

                    this.FireImageReceivedEvent(f);

                    context.Post(o => OnConnectAborted(), null);

                }

            }
            finally
            {
                client.Close();
            }
        }
    }
}
