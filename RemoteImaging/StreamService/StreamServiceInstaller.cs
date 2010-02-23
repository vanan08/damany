using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;


namespace StreamService
{
    [RunInstaller(true)]
    public partial class StreamServiceInstaller : Installer
    {
        public StreamServiceInstaller()
        {
            InitializeComponent();

            ServiceProcessInstaller processInstaller = new ServiceProcessInstaller();
            ServiceInstaller serviceInstaller = new ServiceInstaller();

            processInstaller.Account = ServiceAccount.NetworkService;
            serviceInstaller.DisplayName = "Video Streaming Service";
            serviceInstaller.Description = "远程播放视频文件";
            serviceInstaller.ServiceName = "StreamService";
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
