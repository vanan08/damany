using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatchDog
{
    public interface IHeartBeatMonitor
    {
        TimeSpan TimeToReport { get; set; }
        TimeSpan HoldTimeAfterReport { get; set; }

        event EventHandler HeartBeatStopped;
        void Start();
        void Stop();
    }
}
