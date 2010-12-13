using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.Gateways
{
    public interface IProxyFactory<TIProxy>
    {
        TIProxy Create(System.Net.IPAddress ip);
    }
}
