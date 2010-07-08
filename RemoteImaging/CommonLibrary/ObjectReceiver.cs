using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace Damany.RemoteImaging.Common
{
    public class ObjectReceiver
    {
        public event EventHandler<MiscUtil.EventArgs<object>> ObjectReceived;
        public event EventHandler ExceptionOccurred;

        public ObjectReceiver(System.Net.Sockets.TcpClient socket)
        {
            this.notifier = new System.Threading.Thread(this.DoNotify);
            this.notifier.IsBackground = true;

            this.receiver = new System.Threading.Thread(this.DoReceive);
            this.receiver.IsBackground = true;

            this.notifier.Start();
            this.receiver.Start(socket);
        }

        public void Start()
        {
           
        }

        public void EnqueueObject(object o)
        {
            lock (this.locker)
            {
                ObjectsQueue.Enqueue(o);
                this.goSignal.Set();
            }
        }

        public object DequeueObject()
        {
            lock (this.locker)
            {
                if (ObjectsQueue.Count>0)
                {
                    return ObjectsQueue.Dequeue();
                }

                return null;
            }
        }

        private void DoReceive(object state)
        {
            System.Net.Sockets.TcpClient socket = state as System.Net.Sockets.TcpClient;

            try
            {
                while (!this.done)
                {
                    object o = formatter.Deserialize(socket.GetStream());
                    this.EnqueueObject(o);
                    goSignal.Set();
                }
            }
            catch (System.Exception ex)
            {
                if (this.ExceptionOccurred != null)
                {
                    this.ExceptionOccurred(this, EventArgs.Empty);
                }
            	
            }
            
        }

        private void DoNotify(object state)
        {
            while (!this.done)
            {
                goSignal.WaitOne();
                object o = DequeueObject();
                if (o == null) continue;

                System.Diagnostics.Debug.WriteLine(o.ToString());

                if (this.ObjectReceived != null)
                {
                    this.ObjectReceived(this, new MiscUtil.EventArgs<object>(o));
                }
            }
        }

        Queue<object> ObjectsQueue = new Queue<object>();
        object locker = new object();
        System.Threading.AutoResetEvent goSignal = new System.Threading.AutoResetEvent(false);
        System.Threading.Thread notifier;
        System.Threading.Thread receiver;
        bool done;
        BinaryFormatter formatter = new BinaryFormatter();
    }
}
