using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace Damany.RemoteImaging.Common
{
    public class ObjectSender
    {
        public ObjectSender(System.Net.Sockets.TcpClient socket)
        {
            this.worker = new System.Threading.Thread(this.DoWork);
            this.worker.IsBackground = true;
            this.worker.Start(socket);

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

        private void DoWork(object state)
        {
            System.Net.Sockets.TcpClient socket = state as System.Net.Sockets.TcpClient;

            while (!this.done)
            {
                goSignal.WaitOne();
                object o = DequeueObject();
                if (o == null) continue;

                formatter.Serialize(socket.GetStream(), o);

                
            }
        }

        Queue<object> ObjectsQueue = new Queue<object>();
        object locker = new object();
        System.Threading.AutoResetEvent goSignal = new System.Threading.AutoResetEvent(false);
        System.Threading.Thread worker;
        bool done;
        BinaryFormatter formatter = new BinaryFormatter();
    }
}
