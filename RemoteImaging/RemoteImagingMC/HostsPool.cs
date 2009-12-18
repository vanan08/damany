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
        UdpBus bus;
        BackgroundWorker worker = new BackgroundWorker();

        public HostsPool(string announceIp, int port)
        {
            RaiseListChangedEvents = true;
            
            bus = new UdpBus(announceIp, port);

            bus.Subscrib(Topics.HostConfigReport, HostMessageHandler);

            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.WorkerSupportsCancellation = true;
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!this.worker.CancellationPending)
            {
                this.bus.Publish(Topics.CenterAnnounce, new MonitorCenterAnnounce(), 3000);
                System.Threading.Thread.Sleep(3000);
            }
        }

        public void Start()
        {
            this.bus.Start();
            this.worker.RunWorkerAsync();
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
                    h.Config = config;
                    h.Ip = args.From.Address;
                    h.LastSeen = DateTime.Now;
                    h.Status = HostStatus.OnLine;
                }
                else//add new
                {
                    var h = new Host { Config = config, Ip = args.From.Address, LastSeen = DateTime.Now, Status = HostStatus.OnLine };
                    this.InsertItem(0, h);
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
