using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public interface ILicensePlateObserver
    {
        void LicensePlateCaptured(LicensePlateInfo licensePlateInfo);
    }
}
