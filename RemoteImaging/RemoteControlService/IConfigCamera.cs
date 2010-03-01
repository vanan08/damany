using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Damany.Component;

namespace RemoteControlService
{
    [ServiceContract]
    public interface IConfigCamera
    {

        [OperationContract]
        void SetShutter(int cameraId, ShutterMode mode, int level);

        [OperationContract]
        void SetIris(int cameraId, IrisMode mode, int level);

        [OperationContract]
        void SetAGCMode(int cameraId, bool enableAGC, bool enableDigitalGain);
    }
}
