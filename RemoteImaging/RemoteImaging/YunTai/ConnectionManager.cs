using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RemoteImaging.YunTai
{
    class ConnectionManager
    {
        private readonly Dictionary<Uri, System.IO.Stream> _connections
            = new Dictionary<Uri, Stream>();

        public Stream GetConnection(Uri uri)
        {
            if (!_connections.ContainsKey(uri))
            {
                if (uri.Scheme == Uri.UriSchemeNetTcp)
                {
                    var tcpClient = new System.Net.Sockets.TcpClient();
                    tcpClient.Connect(uri.Host, uri.Port);
                    _connections.Add(uri, tcpClient.GetStream());
                    
                }
                else if (uri.Scheme == Uri.UriSchemeFile)
                {
                    var com = new System.IO.Ports.SerialPort(uri.Host);
                    com.Open();
                    _connections.Add(uri, com.BaseStream);
                }
                else 
                    throw new NotSupportedException("uri is not supported");
            }

            return _connections[uri];
        }
    }
}
