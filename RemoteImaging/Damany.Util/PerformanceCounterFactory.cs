using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Util
{
    public static class PerformanceCounterFactory
    {
        public static System.Diagnostics.PerformanceCounter CreateMemoryCounter()
        {
            return new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes");
        }
    }
}
