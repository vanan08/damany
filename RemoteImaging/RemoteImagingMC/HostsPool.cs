using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Damany.RemoteImaging.Net.Discovery;
using Damany.RemoteImaging.Net.Messages;
using Damany.RemoteImaging.Common;


namespace RemoteImaging
{
    public class HostsPool : BindingList<Host>, IDisposable
    {
        private System.Threading.SynchronizationContext syncCtx;
        UdpBus bus;
        BackgroundWorker worker = new BackgroundWorker();
        object locker = new object();

        public IHostsPoolObserver Observer { get; set; }

        public HostsPool(string announceIp, int port)
        {
            syncCtx = System.Threading.SynchronizationContext.Current;
            RaiseListChangedEvents = true;

            bus = new UdpBus(announceIp, port);

            bus.Subscribe(Topics.HostReply, HostMessageHandler);

            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.WorkerSupportsCancellation = true;

            this.Observer = new NullHostsPoolObserver();
        }

        void UpdateOnLineState()
        {
            foreach (var item in this)
            {
                if ((DateTime.Now - item.LastSeen) > new TimeSpan(0, 1, 0))
                {
                    lock (this.locker)
                        item.Status = HostStatus.OffLine;
                    NotifyUpdateHost(item);
                }
            }

        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!this.worker.CancellationPending)
            {
                this.bus.Publish(Topics.CenterQuery, new MonitorCenterAnnounce(), 3000);
                System.Threading.Thread.Sleep(3000);
                this.UpdateOnLineState();
            }
        }

        public void Start()
        {
            this.bus.Start();
            this.worker.RunWorkerAsync();
        }

        private void NotifyUpdateHost(Host h)
        {
            if (this.syncCtx != null)
                this.syncCtx.Post((o) => this.Observer.UpdateHost((Host)o), h);
            else
                this.Observer.UpdateHost(h);
        }

        private void NotifyAddHost(Host h)
        {
            if (this.syncCtx != null)
                this.syncCtx.Post((o) => this.Observer.AddHost((Host)o), h);
            else
                this.Observer.AddHost(h);
        }


        private void HostMessageHandler(object sender, TopicArgs args)
        {

            if (args.DataObject is HostConfiguration)
            {
                HostConfiguration config = args.DataObject as HostConfiguration;

                //update it 
                if (this.ContainsID(config.ID))
                {
                    Host h = this[config.ID];

                    lock (this.locker)
                    {
                        h.Config = config;
                        h.Ip = args.From.Address;
                        h.LastSeen = DateTime.Now;
                        h.Status = HostStatus.OnLine;
                    }

                    NotifyUpdateHost(h);
                }
                else//add new
                {
                    var h = new Host { Config = config, Ip = args.From.Address, LastSeen = DateTime.Now, Status = HostStatus.OnLine };
                    this.InsertItem(0, h);

                    NotifyAddHost(h);
                }

            }
        }

        public bool ContainsID(object ID)
        {
            var first = this.FirstOrDefault(h => h.Config.ID.Equals(ID));

            if (first == null) return false;

            return object.Equals(first.Config.ID, ID);
        }

        protected override void InsertItem(int index, Host item)
        {
            if (base.Contains(item)) throw new InvalidOperationException("item already exists");
            base.InsertItem(index, item);
        }

        public Host this[object ID]
        {
            get
            {
                var firstMatch = this.FirstOrDefault(h => h.Config.ID.Equals(ID));
                return firstMatch;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.worker.CancelAsync();
            this.bus.Dispose();
        }

        #endregion
    }
}
