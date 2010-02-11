using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Damany.EPolice.Networking.Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Configuration.Encoding = System.Text.Encoding.Unicode;

                TcpListener listener = new TcpListener(10000);
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
            catch (System.Exception ex)
            {
                HandleException(ex);
            }

        }
        private static void HandleException(System.Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
