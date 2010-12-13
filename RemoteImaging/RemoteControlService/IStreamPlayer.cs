using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Drawing;

namespace RemoteControlService
{
    [ServiceContract]
    public interface IStreamPlayer
    {
        [OperationContract]
        bool StreamVideo(string path);

        [OperationContract]
        void Stop();
    }
}
