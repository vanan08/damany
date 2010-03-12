using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Processors
{
    using Damany.Imaging.Contracts;

    public class FramePumper
    {
        public FramePumper()
        {

        }


        public void Start()
        {
            if (this.worker == null)
            {
                this.worker = new System.Threading.Thread(this.Pump);
                this.worker.IsBackground = true;
                this.worker.Start();
            }
        }

        public void Stop()
        {
            this.done = true;
            this.signal.Set();
            this.worker.Join();
        }

        private void Pump()
        {

            if (this.ActionOnFrame == null)
            {
                throw new InvalidOperationException("MotionDetector is null");
            }


            while (!this.done)
            {
                this.signal.WaitOne();

                var frame = this.GetFrameFromQueue();
                if (frame == null) continue;

                this.ActionOnFrame(frame);
            }
        }

        public void EnqueueFrame(Frame f)
        {
            lock (this.locker)
            {
                this.frameQueue.Enqueue(f);
                this.signal.Set();
            }
        }

        private Frame GetFrameFromQueue()
        {
            var f = default(Frame);

            lock (this.locker)
            {
                if (this.frameQueue.Count > 0)
                {
                    f = this.frameQueue.Dequeue();
                }
            }

            return f;
        }

        
        public Action<Frame> ActionOnFrame { get; set; }
        

        System.Threading.AutoResetEvent signal = new System.Threading.AutoResetEvent(false);
        System.Threading.Thread worker;
        bool done;

        object locker = new object();
        Queue<Frame> frameQueue = new Queue<Frame>();
    }
}
