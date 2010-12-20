using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kise.IdCard.Server
{
    public interface ILog
    {
        void Log(LogEntry entry);
    }
}
