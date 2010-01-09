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
        void SetShutter(ShutterMode mode, int level);

        [OperationContract]
        void SetIris(IrisMode mode, int level);

        [OperationContract]
        void SetAGCMode(bool enableAGC, bool enableDigitalGain);
    }
}
