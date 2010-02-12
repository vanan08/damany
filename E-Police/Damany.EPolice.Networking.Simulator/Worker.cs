using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Damany.EPolice.Networking.Simulator
{
    public class Worker
    {
        private TcpListener tcpListener;
        private TcpClient tcpClient;
        System.Threading.Thread workerThread;
        bool exit;

        public void Start()
        {
            exit = false;
            workerThread = new System.Threading.Thread(DoStart);
            workerThread.IsBackground = true;
            workerThread.Start();
        }

        public void Stop()
        {
            if (workerThread == null) return;
            if (!workerThread.IsAlive) return;

            exit = true;
            workerThread.Abort();
        }

        private void DoStart()
        {

            tcpListener = new TcpListener(Configuration.RemotePort);
            tcpListener.Start();

            while (!exit)
            {
                tcpClient = tcpListener.AcceptTcpClient();
                tcpListener.BeginAcceptTcpClient()
                var stream = tcpClient.GetStream();
                var endianStream = new MiscUtil.IO.EndianBinaryWriter(Configuration.EndianBitConverter,
                                                                        stream,
                                                                        Configuration.Encoding);
                try
                {
                    while (!exit)
                    {
                        var pack = PacketGenerator.GetDefaultPacket();

                        var buffer = PacketGenerator.BuildPacket(pack);

                        endianStream.Write((uint)Networking.Packets.PacketType.LicensePlate);
                        endianStream.Write(buffer.Length);

                        stream.Write(buffer, 0, buffer.Length);
                    }

                }
                catch (System.Exception ex)
                {
                    HandleException(ex);
                }
            }

        }


        private void HandleException(Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }


    }
}
