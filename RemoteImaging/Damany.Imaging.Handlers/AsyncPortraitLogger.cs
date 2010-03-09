using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Handlers
{
    public class AsyncPortraitLogger : PortraitsLogger
    {
        public AsyncPortraitLogger(string directory)
            :base(directory)
        {
           
        }

        public override void HandlePortraits(IList<Damany.Imaging.Contracts.Frame> motionFrames, 
            IList<Damany.Imaging.Contracts.Portrait> portraits)
        {
            motionFrames.ToList().ForEach(frame => frame.Dispose());

            lock (this.queueLock)
            {
                this.portraitQueue.Enqueue(portraits);
                this.waitForPortraits.Set();
            }
            
        }

        public override bool WantCopy
        {
            get
            {
                return true;
            }
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override void Initialize()
        {
            base.Initialize();
            worker = new System.Threading.Thread(this.WriteThread);
            worker.IsBackground = true;
            worker.Start();
        }


        private void WriteThread()
        {
            while (true)
            {
                waitForPortraits.WaitOne();


                IList<Damany.Imaging.Contracts.Portrait> portraits = null;
                lock (this.queueLock)
                {
                    portraits = this.portraitQueue.Dequeue();
                }

                base.SavePortraits(portraits);

                portraits.ToList().ForEach(p => p.Dispose());

                System.Threading.Thread.Sleep(1000);
            }
        }


        private Queue<IList<Damany.Imaging.Contracts.Portrait>> portraitQueue
            = new Queue<IList<Damany.Imaging.Contracts.Portrait>>();
        private object queueLock = new object();
        private System.Threading.AutoResetEvent waitForPortraits = 
            new System.Threading.AutoResetEvent(false);
        protected System.Threading.Thread worker;
    }
}
