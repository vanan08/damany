using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Handlers
{
    public class AsyncPortraitLogger : PortraitsLogger
    {

        public AsyncPortraitLogger(string directory)
            : base(directory)
        {
            this.wantCopy = true;
            this.wantFrame = false;
            this.name = "Async Portrait Saver";
            this.autoRemove = false;
        }

        public override void HandlePortraits(IList<Damany.Imaging.Contracts.Frame> motionFrames,
            IList<Damany.Imaging.Contracts.Portrait> portraits)
        {
            if (!this.running || this.faulted)
            {
                portraits.ToList().ForEach(p => p.Dispose());
                return;
            }

            lock (this.locker)
            {
                this.portraitQueue.Enqueue(portraits);
                this.signal.Set();
            }

        }

        public override void Start()
        {
            lock (this.locker)
            {
                if (!this.running)
                {
                    worker = new System.Threading.Thread(this.WriteThread);
                    worker.IsBackground = true;

                    this.signal.Reset();
                    this.faulted = false;
                    this.running = true;
                    this.worker.Start();

                    System.Diagnostics.Debug.WriteLine("started");
                }
            }

        }

        public override void Stop()
        {
            lock (this.locker)
            {
                if (this.running)
                {
                    this.running = false;
                    signal.Set();
                    System.Diagnostics.Debug.WriteLine("stopped");
                }


            }
        }

        public override void Initialize()
        {
            base.Initialize();
        }


        private void WriteThread()
        {
            System.Exception error = null;

            try
            {
                while (running)
                {
                    signal.WaitOne();

                    IList<Damany.Imaging.Contracts.Portrait> portraits = null;
                    lock (this.locker)
                    {
                        if (this.portraitQueue.Count > 0)
                        {
                            portraits = this.portraitQueue.Dequeue();
                        }
                    }

                    if (portraits != null)
                    {
                        base.SavePortraits(portraits);
                        portraits.ToList().ForEach(p =>
                        {
                            using (var w = new OpenCvSharp.CvWindow(p.CapturedAt.ToShortTimeString(), p.PortraitImage))
                            {
                                OpenCvSharp.CvWindow.WaitKey(500);
                            }
                            p.Dispose();
                        });
                    }

                }
            }
            catch (System.Exception ex)
            {
                this.faulted = true;
                error = ex;
            }
            finally
            {
                OnStopped(new MiscUtil.EventArgs<Exception>(error));
                this.running = false;
            }

        }

        private void CleanUp()
        {
            lock (this.locker)
            {
                while (this.portraitQueue.Count > 0)
                {
                    this.portraitQueue.Dequeue().ToList().ForEach(p => p.Dispose());
                }
            }
        }


        private Queue<IList<Damany.Imaging.Contracts.Portrait>> portraitQueue
            = new Queue<IList<Damany.Imaging.Contracts.Portrait>>();
        private object locker = new object();
        private System.Threading.AutoResetEvent signal =
            new System.Threading.AutoResetEvent(false);
        protected System.Threading.Thread worker;
        protected bool running;
        private bool faulted;
    }
}
