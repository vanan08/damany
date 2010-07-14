using System;
using log4net;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WatchDog
{
    class HeartBeatMonitorListener
    {
        private readonly IHeartBeatMonitor _beatMonitor;
        private readonly ILog _logger = LogManager.GetLogger(typeof(HeartBeatMonitorBase));
        const int SecondsToWait = 10 * 1000;

        public HeartBeatMonitorListener(IHeartBeatMonitor beatMonitor)
        {
            _beatMonitor = beatMonitor;
        }

        public void Start()
        {
            _beatMonitor.HeartBeatStopped += _beatMonitor_HeartBeatStopped;
            _beatMonitor.Start();

            //reboot monitor
            if (RebootEnabled)
            {
                Task.Factory.StartNew(() =>
                                      {
                                          while (true)
                                          {
                                              var time = DateTime.Now;

                                              if (time.Hour == HourToReboot.Hour &&
                                                  time.Minute >= HourToReboot.Minute && time.Minute <= HourToReboot.Minute + 1)
                                              {
                                                  try
                                                  {
                                                      Process.Start(ShutdownCommand, ShutdownCommandParameter);
                                                  }
                                                  catch (Exception ex)
                                                  {
                                                      _logger.Error("重启系统失败");
                                                      _logger.Error(ex.Message, ex);
                                                  }

                                              }

                                              System.Threading.Thread.Sleep(30 * 1000);
                                          }
                                      });
            }


        }


        public void Stop()
        {
            _beatMonitor.Stop();
        }


        public string ApplicationToReboot { get; set; }
        public DateTime HourToReboot { get; set; }
        public string ShutdownCommand { get; set; }
        public string ShutdownCommandParameter { get; set; }
        public bool RebootEnabled { get; set; }

        void _beatMonitor_HeartBeatStopped(object sender, EventArgs e)
        {
            _logger.Info("Heat Beating Stopped, try to reboot application");

            try
            {
                EnsureKillProcess();
                EnsureStartProcess();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }

        }

        private void EnsureKillProcess()
        {
            var closeAttempts = 0;
            while (true)
            {
                var process = GetProcess();
                if (process != null)
                {
                    if (closeAttempts >= 3)
                    {
                        process.Kill();
                        _logger.Info("close process failed after retries, kill it brutally");
                    }
                    else
                    {
                        _logger.Info("issue command to close process");
                        CloseProcess(process);
                        process.CloseMainWindow();
                        closeAttempts++;
                        _logger.Info("close process succeed");
                    }

                    System.Threading.Thread.Sleep(SecondsToWait);

                }
                else
                {
                    break;
                }

            }
        }

        private void EnsureStartProcess()
        {
            while (true)
            {
                _logger.Info("issue command to start process");

                var workingDir = System.IO.Path.GetDirectoryName(ApplicationToReboot);

                var info = new ProcessStartInfo();
                info.FileName = ApplicationToReboot;
                info.WorkingDirectory = workingDir;

                Process.Start(info);

                _logger.Info("start process succeed");
                System.Threading.Thread.Sleep(SecondsToWait);

                var process = GetProcess();
                if (process != null)
                {
                    break;
                }
            }
        }

        private Process GetProcess()
        {
            var processName = System.IO.Path.GetFileNameWithoutExtension(ApplicationToReboot);

            var msg = "search process with name " + processName;

            try
            {
                var process = Process.GetProcessesByName(processName);
                if (process.Length > 0)
                {
                    msg += " found";
                    return process[0];
                }

                msg += " not found";
                return null;
            }
            finally
            {
                _logger.Info(msg);
            }

        }

        private void CloseProcess(Process process)
        {
            process.CloseMainWindow();
            _logger.Info("issued command to close " + process.ProcessName);
        }
    }
}