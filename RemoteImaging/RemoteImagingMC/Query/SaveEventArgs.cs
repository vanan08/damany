using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.Query
{
    public class SaveEventArgs : EventArgs
    {
        public string TargetDirectory { get; set; }
    }
}
