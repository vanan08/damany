using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Sockets;
using System.Drawing;
using System.Threading;

namespace RemoteImaging.RealtimeDisplay
{
    class LiveClient
    {
        TcpClient client;
        BinaryFormatter formatter;

        public event EventHandler<ImageCapturedEventArgs> ImageReceived;
        public event EventHandler ConnectAborted;
        SynchronizationContext context;


        public object Tag { get; set; }

        public LiveClient(TcpClient client)
        {
            context = SynchronizationContext.Current;

            this.client = client;
            formatter = new BinaryFormatter();
        }


        void FireImageReceivedEvent(Image img)
        {
            if (this.ImageReceived != null)
            {
                ImageCapturedEventArgs args = new ImageCapturedEventArgs
                {
                    ImageCaptured = img,
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
                    Image img = (Image)formatter.Deserialize(client.GetStream());
                    imgSize = img.Size;
                    this.FireImageReceivedEvent(img);
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

                    this.FireImageReceivedEvent(bmp);

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
