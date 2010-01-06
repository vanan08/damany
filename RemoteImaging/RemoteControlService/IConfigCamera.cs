using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace RemoteControlService
{
    [ServiceContract]
    public interface IConfigCamera
    {
        [OperationContract]
        void SetShutterSpeed(int speed);

        [OperationContract]
        void SetShutterMode(ShutterMode mode);

        [OperationContract]
        void SetIrisMode(IrisMode mode);

        [OperationContract]
        void SetIrisLevel(int level);

        [OperationContract]
        void SetAGCMode(bool enableAGC, bool enableDigitalGain);
    }
}
