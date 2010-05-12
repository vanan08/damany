using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public interface ILicensePlateEventPublisher
    {
        void RegisterForLicensePlateEvent(ILicensePlateObserver licensePlateObserver);
        void PublishLicensePlate(LicensePlateInfo licensePlateInfo);
    }
}
