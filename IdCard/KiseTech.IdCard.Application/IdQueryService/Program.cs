using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Kise.IdCard.Messaging.Link;
using RBSPAdapter_COM;

namespace IdQueryService
{
    class Program
    {
        private static TcpServerLink _server;
        static void Main(string[] args)
        {
            RSBPAdapterCOMObjClass _q;
            try
            {
                _server = new Kise.IdCard.Messaging.Link.TcpServerLink();
                _server.NewMessageReceived += new EventHandler<MiscUtil.EventArgs<Kise.IdCard.Messaging.IncomingMessage>>(server_NewMessageReceived);
                _server.Start();
            }
            finally
            {
                //if (_q != null)
                {
                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(_q);
                }
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        static void server_NewMessageReceived(object sender, MiscUtil.EventArgs<Kise.IdCard.Messaging.IncomingMessage> e)
        {
            Console.WriteLine("------received query----------\r\n");
            //if (_q == null)
            RSBPAdapterCOMObjClass _q;
            _q = new RSBPAdapterCOMObjClass();
            _q.queryCondition = string.Format("sfzh='{0}'", e.Value.Message);
            _q.queryType = "QueryQGRK";
            var result = _q.queryCondition;
            Console.WriteLine(result + "\r\n" + "---------------");
            _server.SendAsync("dfdfdf", "normal query result" + DateTime.Now);

            _q.queryCondition = string.Format("sfzh='{0}'", e.Value.Message);
            _q.queryType = "QueryZTK";
            result = _q.queryCondition;
            Console.WriteLine(result + "\r\n" + "---------------");
            _server.SendAsync("dfdfdf", "suspect query result" + DateTime.Now);

            System.Runtime.InteropServices.Marshal.ReleaseComObject(_q);

        }
    }
}
