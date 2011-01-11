using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging
{
    public interface IHostsPoolObserver
    {
        void AddHost(Host h);
        void UpdateHost(Host h);
    }
}
