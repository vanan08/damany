using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Util
{
    public class PersistentWorker
    {
        public PersistentWorker()
        {
            this.WorkFrequency = 2;
            this.RetryInterval = 3000;

            this.timer.AutoReset = false;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            this.OnRetrySucceedDo = delegate { };

        }

        public Action DoWork { get; set; }
        public Action OnExceptionRetry { get; set; }
        public Action OnRetrySucceedDo { get; set; }

        public event Action<object> OnWorkItemIsDone;
        public event Action<Exception> OnException;

        public void Start()
        {
            this.timer.Enabled = true;
        }

        public void Stop()
        {
            this.done = true;
            this.stopped.WaitOne();

        }

        public void ReportWorkItem(object item)
        {
            if (this.OnWorkItemIsDone != null)
            {
                this.OnWorkItemIsDone(item);
            }
        }

        public float WorkFrequency
        {
            get
            {
                return (float)(1000d / this.timer.Interval);
            }
            set
            {
                this.timer.Interval = (1000d / value);
            }
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (this.IsBusy) return;

                this.IsBusy = true;
                this.DoWork();
                this.IsBusy = false;
                this.timer.Enabled = !this.done;
                if (!this.done)
                {
                    this.stopped.Set();
                }
            }
            catch (System.Exception ex)
            {
                Recover();
            }

        }


        private void Recover()
        {
            while (!this.done)
            {
                try
                {
                    this.OnExceptionRetry();
                    this.OnRetrySucceedDo();
                    this.timer.Enabled = true;
                    break;
                }
                catch (System.Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("sleep {0} to retry", this.RetryInterval));
                    System.Threading.Thread.Sleep(RetryInterval);
                }

            }
        }

        public int RetryInterval { get; set; }
        public bool IsBusy
        {
            get
            {
                lock (this.locker)
                {
                    return this.busy;
                }
            }
            set
            {
                lock (this.locker)
                {
                    this.busy = value;
                }
            }
        }

        System.Timers.Timer timer = new System.Timers.Timer();
        bool done;
        System.Threading.AutoResetEvent stopped = new System.Threading.AutoResetEvent(false);
        object locker = new object();
        bool busy;

    }
}
