using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using RemoteControlService;

namespace RemoteImaging.Service
{
    class ServiceProvider
    {
        MotionDetectWrapper.MotionDetector motionDetector;
        FaceSearchWrapper.FaceSearch faceSearcher;
        RealtimeDisplay.Presenter presenter;

        public ServiceProvider(
                    MotionDetectWrapper.MotionDetector motionDetector,
                    FaceSearchWrapper.FaceSearch faceSearcher,
                    RealtimeDisplay.Presenter presenter )
        {
            this.motionDetector = motionDetector;
            this.faceSearcher = faceSearcher;
            this.presenter = presenter;
        }


        public void OpenService()
        {
            string baseAddress = string.Format("net.tcp://{0}:8000", System.Net.IPAddress.Any);

            Uri netTcpBaseAddress = new Uri(baseAddress);
            ServiceHost host = new ServiceHost(typeof(Service.SearchProvider), netTcpBaseAddress);

            NetTcpBinding tcpBinding = BindingFactory.CreateNetTcpBinding();

            host.AddServiceEndpoint(
                typeof(RemoteControlService.ISearch),
                tcpBinding, "TcpService");

            host.Open();
        }
    }
}
