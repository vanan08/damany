using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging
{
    public interface ICameraNameResolver
    {
        string GetName(int cameraId);
    }
}
