using System;
using System.Threading;

namespace WatchDog
{
    class MockHeartBeatMonitor : HeartBeatMonitorBase
    {
        protected override void Work(CancellationToken token)
        {
            while (true)
            {
                token.ThrowIfCancellationRequested();
                Thread.Sleep(TimeToReport);
                InvokeHeartBeatStopped(EventArgs.Empty);
            }
        }
    }
}