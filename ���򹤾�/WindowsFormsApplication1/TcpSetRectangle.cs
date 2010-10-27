using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Net.Sockets;
using MiscUtil.IO;

namespace WindowsFormsApplication1
{
    class TcpSetRectangle : ISetRectangle
    {
        private readonly string _ip;
        private readonly int _port;
        private TcpClient _tcpClient;
        private EndianBinaryWriter _writer;

        public TcpSetRectangle(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }

        public void Set(Rectangle rectangle, Action<Exception> callBack)
        {
            var w = Task.Factory.StartNew(() =>
                                              {

                                                  _tcpClient = new TcpClient();
                                                  _tcpClient.Connect(_ip, _port);

                                                  _writer = new EndianBinaryWriter(new MiscUtil.Conversion.BigEndianBitConverter(),
                                                      _tcpClient.GetStream());

                                                  _writer.Write((uint)1);
                                                  _writer.Write((uint)rectangle.Left);
                                                  _writer.Write((uint)rectangle.Top);
                                                  _writer.Write((uint)rectangle.Width);
                                                  _writer.Write((uint)rectangle.Height);
                                                  _writer.Write((uint)150);

                                                  _writer.Flush();

                                                  _tcpClient.Client.Shutdown(SocketShutdown.Both);
                                                  _tcpClient.Close();

                                              });

            w.ContinueWith(ant =>
                               {
                                   Exception ex = null;
                                   if (ant.Exception != null)
                                   {
                                       ex = ant.Exception.InnerException;
                                   }
                                   callBack(ex);
                               });

        }
    }
}