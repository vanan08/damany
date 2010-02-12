using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Damany.EPolice.Networking.Simulator
{
    public class Worker
    {
        public void Start()
        {
            Action start = DoStart;
            start.BeginInvoke(null, null);
        }

        private void DoStart()
        {

            TcpListener listener = new TcpListener(Configuration.RemotePort);
            listener.Start();

            while (true)
            {
                var client = listener.AcceptTcpClient();
                var stream = client.GetStream();
                var endianStream = new MiscUtil.IO.EndianBinaryWriter(Configuration.EndianBitConverter,
                                                                        stream,
                                                                        Configuration.Encoding);
                try
                {
                    while (true)
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
