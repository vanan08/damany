using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using RemoteControlService;

namespace StreamService
{
    public partial class StreamingService : ServiceBase
    {
        private ServiceHost host;


        public StreamingService()
        {
            InitializeComponent();
            this.ServiceName = "StreamService";
            this.CanHandlePowerEvent = false;
            this.CanStop = true;
            this.CanShutdown = true;
        }

        protected override void OnStart(string[] args)
        {

            string baseAddress = string.Format("net.tcp://{0}:{1}", System.Net.IPAddress.Any, Properties.Settings.Default.PortToListen);
            Uri netTcpBaseAddress = new Uri(baseAddress);

            VLCServer server = new VLCServer(this.EventLog);

            host = new ServiceHost(server, netTcpBaseAddress);

            NetTcpBinding tcpBinding = BindingFactory.CreateNetTcpBinding();

            host.AddServiceEndpoint(typeof(RemoteControlService.IStreamPlayer),
                tcpBinding, "TcpService");

            host.Open();

            this.EventLog.WriteEntry("Stream Service Started Successfully On Port: " + 
                Properties.Settings.Default.PortToListen.ToString());
        }

        protected override void OnStop()
        {
            if (host != null)
            {
                host.Close();
            }

            host = null;
        }
    }
}
