using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.RemoteImaging.Net.Discovery;
using Damany.RemoteImaging.Net.Messages;
using Damany.RemoteImaging.Common;

namespace RemoteImaging
{
    public class Communication : IDisposable
    {
        UdpBus bus;

        public Communication(string ip, int port)
        {
            bus = new UdpBus(ip, port);
            bus.Subscribe(Topics.CenterQuery, OnCenterQuery);
        }

        public void Start()
        {
            this.bus.Start();
        }

        private void OnCenterQuery(object s, TopicArgs args)
        {
            if (args.DataObject is HostConfigurationQuery)
            {
                var cfg = new HostConfiguration(Configuration.Instance.GetStationID());
                cfg.CameraID = 2;
                cfg.Name = Properties.Settings.Default.HostName;
                cfg.TotalStorageCapacityMB = new FileSystemStorage(Properties.Settings.Default.OutputPath).GetTotalStorageMB();
                cfg.ReservedStorageCapacityMB = Configuration.Instance.GetReservedSpaceinMB();
                bus.Publish(Topics.HostReply, cfg, 3000);
            }


        }


        #region IDisposable Members

        public void Dispose()
        {
            if (this.bus != null) this.bus.Dispose();
        }

        #endregion
    }
}
