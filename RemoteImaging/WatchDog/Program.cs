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

                    var monitor = new UdpHeartBeatMonitor(9999);
                    monitor.TimeToReport = new TimeSpan(0, 0, 5);

                    s.Named("WatchDog");
                    s.HowToBuildService(name => new HeartBeatMonitorListener(monitor));
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
