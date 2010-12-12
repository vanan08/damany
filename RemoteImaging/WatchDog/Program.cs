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
                    IHeartBeatMonitor monitor;

                    monitor = CreateFileMonitor();

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
