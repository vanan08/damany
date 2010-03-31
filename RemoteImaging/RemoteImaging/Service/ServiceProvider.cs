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
        //MotionDetectWrapper.MotionDetector motionDetector;
        FaceSearchWrapper.FaceSearch faceSearcher;
        Damany.Component.ICamera camera;

        public ServiceProvider(
                    //MotionDetectWrapper.MotionDetector motionDetector,
                    FaceSearchWrapper.FaceSearch faceSearcher,
                    Damany.Component.ICamera camera)
        {
//             if (motionDetector == null)
//                 throw new ArgumentNullException("motionDetector", "motionDetector is null.");
            if (faceSearcher == null)
                throw new ArgumentNullException("faceSearcher", "faceSearcher is null.");
            if (camera == null)
                throw new ArgumentNullException("camera", "camera is null.");


            //this.motionDetector = motionDetector;
            this.faceSearcher = faceSearcher;
            this.camera = camera;
        }


        private static void DoOpenService(string baseAddress, Type serviceType, Type implementedContract)
        {
            DoOpenService(baseAddress, null, serviceType, implementedContract);
        }


        private static void DoOpenService(string baseAddress, object instance, Type implementedContract)
        {
            DoOpenService(baseAddress, instance, null, implementedContract);
        }


        private static void DoOpenService(
            string baseAddress,
            object instance,
            Type serviceType, 
            Type implementedContract)
        {
            Uri netTcpBaseAddress = new Uri(baseAddress);

            ServiceHost host = null;
            if (instance != null)
            {
                host = new ServiceHost(instance, netTcpBaseAddress);
            }
            else
            {
                host = new ServiceHost(serviceType , netTcpBaseAddress);
            }

            NetTcpBinding tcpBinding = BindingFactory.CreateNetTcpBinding();

            host.AddServiceEndpoint(
                implementedContract,
                tcpBinding, "TcpService");

            host.Open();
        }

        private static void OpenSearchService()
        {
            var baseAddressSearch = string.Format("net.tcp://{0}:8000", System.Net.IPAddress.Any);
            DoOpenService(baseAddressSearch, typeof(Service.SearchProvider), typeof(RemoteControlService.ISearch));
        }

        private void OpenConfigHostService()
        {
            var confitHost =
                            new Service.ConfigHostProvider(this.faceSearcher);
            var baseAddrConfigHost = string.Format("net.tcp://{0}:8001", System.Net.IPAddress.Any);
            DoOpenService(baseAddrConfigHost, confitHost, typeof(RemoteControlService.IConfigHost));
        }


        private void OpenConfigCameraService()
        {
            var configCamera = new Service.ConfigCameraProvider(this.camera);
            var baseAddr = string.Format("net.tcp://{0}:8002", System.Net.IPAddress.Any);
            DoOpenService(baseAddr, configCamera, typeof(RemoteControlService.IConfigCamera));
        }


        public void OpenServices()
        {
            OpenSearchService();

            OpenConfigHostService();

            OpenConfigCameraService();
        }
    }
}
