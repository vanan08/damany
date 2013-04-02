using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.ServiceModel;
using System.Text;
using Kise.IdCard.Messaging.Link;
using RBSPAdapter_COM;

namespace IdQueryService
{
    class Program
    {
        static void Main(string[] args)
        {
            var queryProvider = new IdQueryProvider();
            queryProvider.QueryIdCard(args[0]);

            //var channel = new TcpServerChannel(Properties.Settings.Default.TcpPortNo);

            //ChannelServices.RegisterChannel(channel, false);

            //var remObj = new WellKnownServiceTypeEntry
            //(
            //    typeof(IdQueryProvider),
            //    "IdQueryService",
            //    WellKnownObjectMode.SingleCall
            //);

            //RemotingConfiguration.RegisterWellKnownServiceType(remObj);

            Console.WriteLine("Press [ENTER] to exit.");

            Console.ReadLine();
        }
    }
}
