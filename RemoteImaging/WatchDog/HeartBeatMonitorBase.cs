using System;
using System.Threading;
using System.Threading.Tasks;

namespace WatchDog
{
    public abstract class HeartBeatMonitorBase : IHeartBeatMonitor
    {
        public TimeSpan TimeToReport { get; set; }
        public event EventHandler HeartBeatStopped;
        public event EventHandler OnStart;
        public event EventHandler OnStop;

        public void InvokeOnStop(EventArgs e)
        {
            EventHandler handler = OnStop;
            if (handler != null) handler(this, e);
        }

        public void InvokeOnStart(EventArgs e)
        {
            EventHandler handler = OnStart;
            if (handler != null) handler(this, e);
        }

        public void InvokeHeartBeatStopped(EventArgs e)
        {
            EventHandler handler = HeartBeatStopped;
            if (handler != null) handler(this, e);
        }


        public void Start()
        {
            if (!_started)
            {
                this.InvokeOnStart(EventArgs.Empty);

                _cancelTokenSource = new CancellationTokenSource();
                var token = _cancelTokenSource.Token;

                Task.Factory.StartNew(() =>
                                          {
                                              try
                                              {
                                                  Work(token);
                                              }
                                              catch (OperationCanceledException) { }

                                              this.InvokeOnStop(EventArgs.Empty);

                                          }, token);
                _started = true;
            }


        }

        protected abstract void Work(CancellationToken token);

        public void Stop()
        {
            if (_started && _cancelTokenSource != null)
            {
                _cancelTokenSource.Cancel();
            }
        }

        private bool _started;
        private CancellationTokenSource _cancelTokenSource;
    }
}