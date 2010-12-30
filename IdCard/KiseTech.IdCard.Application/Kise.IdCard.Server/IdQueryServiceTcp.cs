using System;
using System.Threading.Tasks;
using Kise.IdCard.Messaging.Link;

namespace Kise.IdCard.Server
{
    class IdQueryServiceTcp : IIdQueryService
    {
        private Messaging.Link.TcpClientLink _tcp;

        public int PortToConnect { get; set; }

        public async Task<string> QueryIdCardAsync(string queryType, string queryString)
        {
            //TaskEx.Run(() =>
            //               {
            //                   if (_tcp == null)
            //                   {
            //                       _tcp = new TcpClientLink();
            //                       _tcp.PortToConnect = PortToConnect;
            //                       await
            //                       _tcp.Start();
            //                   }

            //                   // _tcp.
            //               }
            //    );
            throw new NotImplementedException();
        }

        public string QueryIdCard(string queryType, string queryString)
        {
            throw new NotImplementedException();
        }
    }
}