using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace WatchDog
{
    public partial class MonitorService : ServiceBase
    {
        private HeartBeatMonitorListener _listener;

        public MonitorService()
        {
            InitializeComponent();

            IHeartBeatMonitor monitor;

            monitor = CreateFileMonitor();

            var timeString = Properties.Settings.Default.HourToReboot.Split(':');
            var h = int.Parse(timeString[0]);
            var min = int.Parse(timeString[1]);

            _listener = new HeartBeatMonitorListener(monitor);
            _listener.HourToReboot = new DateTime(1974, 12, 31, h, min, 0);
            _listener.ApplicationToReboot = Properties.Settings.Default.ApplicationToReboot;
            _listener.ShutdownCommand = Properties.Settings.Default.ShutDownCommand;
            _listener.ShutdownCommandParameter = Properties.Settings.Default.ShutDownCommandParameter;
            _listener.RebootEnabled = Properties.Settings.Default.EnableReboot;
        }

        protected override void OnStart(string[] args)
        {
            _listener.Start();
        }

        protected override void OnStop()
        {
            _listener.Stop();
        }


        private static IHeartBeatMonitor CreateUdpMonitor()
        {
            var monitor = new UdpHeartBeatMonitor(Properties.Settings.Default.PortToListen);
            InitializeMonitor(monitor);

            return monitor;
        }


        private static IHeartBeatMonitor CreateFileMonitor()
        {
            var m = new FileSystemMonitor(Properties.Settings.Default.DirectoryToWatch);
            InitializeMonitor(m);

            var extensions = Properties.Settings.Default.FileExtensionsToMonitor.Split(new[] { ';' },
                                                                                       StringSplitOptions.
                                                                                           RemoveEmptyEntries);

            Array.ForEach(extensions, e => m.ExtensionsToMonitor.Add(e));

            return m;
        }

        private static void InitializeMonitor(IHeartBeatMonitor monitor)
        {
            monitor.TimeToReport = new TimeSpan(0, 0, Properties.Settings.Default.SecondsToReport);
            monitor.HoldTimeAfterReport = new TimeSpan(0, 0, Properties.Settings.Default.SecondsToHoldAfterReport);
        }

    }
}
