using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.IO;

namespace WatchDog
{
    class FileSystemMonitor : HeartBeatMonitorBase
    {
        private readonly string _directoryToMonitor;
        private readonly List<string> _extensionsToMonitor = new List<string>();
        private DateTime _lastActiveTime;
        private readonly object _locker = new object();
        private FileSystemWatcher _watcher;




        public IList<string> ExtensionsToMonitor
        {
            get { return _extensionsToMonitor; }
        }

        private DateTime LastActiveTime
        {
            get
            {
                lock (_locker)
                {
                    return _lastActiveTime;
                }

            }
            set
            {
                lock (_locker)
                {
                    _lastActiveTime = value;
                }

            }
        }

        public FileSystemMonitor(string directoryToMonitor)
        {
            if (!System.IO.Directory.Exists(directoryToMonitor))
            {
                throw new ArgumentException(directoryToMonitor + " doesn't exists");
            }

            _directoryToMonitor = directoryToMonitor;

            this.OnStart += FileSystemMonitor_OnStart;
            this.OnStop += new EventHandler(FileSystemMonitor_OnStop);
        }

        void FileSystemMonitor_OnStop(object sender, EventArgs e)
        {
            if (_watcher != null)
            {
                _watcher.EnableRaisingEvents = false;
            }
        }

        void FileSystemMonitor_OnStart(object sender, EventArgs e)
        {
            if (_watcher == null)
            {
                _watcher = new FileSystemWatcher(_directoryToMonitor);
                _watcher.NotifyFilter = NotifyFilters.FileName;
                _watcher.Created += watcher_Created;
                _watcher.IncludeSubdirectories = true;
            }

            _watcher.EnableRaisingEvents = true;

            LastActiveTime = DateTime.Now.AddSeconds(15);
        }

        void watcher_Created(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                var extension = System.IO.Path.GetExtension(e.Name);

                var interested = _extensionsToMonitor.Where(s => string.Compare(s, extension, true) == 0).Count() > 0;

                if (interested)
                {
                    LastActiveTime = DateTime.Now;
                }
            }
        }

        protected override void Work(CancellationToken token)
        {
            while (true)
            {
                token.ThrowIfCancellationRequested();

                var now = DateTime.Now;
                if (now - LastActiveTime > TimeToReport)
                {
                    InvokeHeartBeatStopped(EventArgs.Empty);
                    LastActiveTime = DateTime.Now + HoldTimeAfterReport;
                }

                Thread.Sleep(10 * 1000);

            }
        }
    }
}