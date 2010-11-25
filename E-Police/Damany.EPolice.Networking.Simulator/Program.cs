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
            Configuration.RemotePort = 10000;
            Configuration.Encoding = System.Text.Encoding.Unicode;
            Configuration.EndianBitConverter = MiscUtil.Conversion.BigEndianBitConverter.Big;

            var worker = new Worker();
            worker.Start();

            System.Console.ReadKey();


        }
    }
}
