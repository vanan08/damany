using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace RemoteControlService
{
    [ServiceContract]
    public interface IConfigHost
    {
        [OperationContract]
        void SetHostName(string name);

        [OperationContract]
        void SetForbiddenRegion(System.Drawing.Rectangle rect);

        [OperationContract]
        void UpdateBackgroundImage();

        [OperationContract]
        void SetReservedDiskSpaceMB(int capacity);
    }
}
