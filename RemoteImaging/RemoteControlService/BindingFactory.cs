using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Xml;

namespace RemoteControlService
{
    public class BindingFactory
    {
        public static NetTcpBinding CreateNetTcpBinding()
        {
            int messageSize = int.MaxValue;

            XmlDictionaryReaderQuotas readerQuotas =
                new XmlDictionaryReaderQuotas();
            readerQuotas.MaxArrayLength = messageSize;


            NetTcpBinding tcpBinding = new NetTcpBinding(SecurityMode.None);
            tcpBinding.MaxReceivedMessageSize = messageSize;
            tcpBinding.ReaderQuotas = readerQuotas;
            tcpBinding.TransferMode = TransferMode.Buffered;

            return tcpBinding;
        }
    }
}
