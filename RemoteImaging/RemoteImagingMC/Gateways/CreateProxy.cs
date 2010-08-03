using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.Gateways
{
    public delegate TIProxy CreateProxy<TIProxy>(System.Net.IPAddress ip);
}
