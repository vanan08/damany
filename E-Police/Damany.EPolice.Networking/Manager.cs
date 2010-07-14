using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.EPolice.Networking.Parsers;
using System.Net.Sockets;

namespace Damany.EPolice.Networking
{

    public class Manager
    {

        public Manager(ISplitter splitter)
        {
            this.splitter = splitter;
            this.parsers = new List<IParser>();

        }

        public void Start()
        {
            clientSocket = new System.Net.Sockets.TcpClient();
            clientSocket.Connect(System.Net.IPAddress.Parse(Configuration.RemoteIp), Configuration.RemotePort);

            workerThread = new System.Threading.Thread(this.StartInternal);
            workerThread.IsBackground = true;
            workerThread.Start(clientSocket.GetStream());
        }

        public void Stop()
        {
            if (workerThread == null) return;
            if (!workerThread.IsAlive) return;

            clientSocket.Client.Shutdown(SocketShutdown.Both);
            workerThread.Join();
        }

        public IList<IParser> Parsers
        {
            get
            {
                return parsers;
            }
        }

        private void StartInternal(object userData)
        {
            System.IO.Stream stream = (System.IO.Stream) userData;
            try
            {
                while (true)
                {
                    var packetBuffer = this.splitter.ReadNext(stream);
                    HandlePacket(packetBuffer);
                }
            }
            catch (System.IO.IOException ex)
            {
                if (ex.InnerException is System.Net.Sockets.SocketException)
                {
                    var sockEx = ex.InnerException as System.Net.Sockets.SocketException;
                    if (sockEx.ErrorCode != (int)SocketError.ConnectionAborted)
                    {
                        throw;
                    }
                }

               
            }
           
        }

        private void HandlePacket(Damany.EPolice.Networking.Packets.BinaryPacket packetBuffer)
        {
            foreach (var parser in this.Parsers)
            {
                if (parser.CanParse(packetBuffer))
                {
                    parser.Parse(packetBuffer);
                    parser.NotifyListener();
                    return;
                }
            }
        }

        private TcpClient clientSocket;
        private System.Threading.Thread workerThread;
        private IList<IParser> parsers;
        private ISplitter splitter;

    }
}
