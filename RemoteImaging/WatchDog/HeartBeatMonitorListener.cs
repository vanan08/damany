using System;

namespace WatchDog
{
    class HeartBeatMonitorListener
    {
        private readonly IHeartBeatMonitor _beatMonitor;

        public HeartBeatMonitorListener(IHeartBeatMonitor beatMonitor)
        {
            _beatMonitor = beatMonitor;
        }

        public void Start()
        {
            _beatMonitor.HeartBeatStopped += _beatMonitor_HeartBeatStopped;
            _beatMonitor.Start();
        }


        public void Stop()
        {
            _beatMonitor.Stop();
        }

        void _beatMonitor_HeartBeatStopped(object sender, EventArgs e)
        {
            Console.WriteLine("heart beating stopped at " + DateTime.Now);
        }

    }
}