using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.RemoteImaging.Common
{
    public interface IProgress : IDisposable
    {
        int Percent { set; }
    }
}
