using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Topshelf;
using Topshelf.Configuration;
using Topshelf.Configuration.Dsl;

namespace WatchDog
{
    class Program
    {
        static void Main(string[] args)
        {
            RunConfiguration cfg = RunnerConfigurator.New(x =>
            {
                x.ConfigureService<HeartBeatMonitorListener>(s =>
                {
                    var monitor = new UdpHeartBeatMonitor(Properties.Settings.Default.PortToListen);
                    monitor.TimeToReport = new TimeSpan(0, 0, Properties.Settings.Default.SecondsToReport);
                    monitor.HoldTimeAfterReport = new TimeSpan(0, 0, Properties.Settings.Default.SecondsToHoldAfterReport);

                    var timeString = Properties.Settings.Default.HourToReboot.Split(':');
                    var h = int.Parse(timeString[0]);
                    var min = int.Parse(timeString[1]);

                    var listener = new HeartBeatMonitorListener(monitor);
                    listener.HourToReboot = new DateTime(1974, 12, 31, h, min, 0);
                    listener.ApplicationToReboot = Properties.Settings.Default.ApplicationToReboot;
                    listener.ShutdownCommand = Properties.Settings.Default.ShutDownCommand;
                    listener.ShutdownCommandParameter = Properties.Settings.Default.ShutDownCommandParameter;
                    listener.RebootEnabled = Properties.Settings.Default.EnableReboot;

                    s.Named("WatchDog");
                    s.HowToBuildService(name => listener);
                    s.WhenStarted(m => m.Start());
                    s.WhenStopped(m => m.Stop());
                });

                x.RunAsLocalSystem();

                x.SetDescription("Face Capture Watch dog Host");
                x.SetDisplayName("Face Capture Watch dog");
                x.SetServiceName("Face Capture Watch dog");
            });

            Runner.Host(cfg, args);

        }
    }
}
