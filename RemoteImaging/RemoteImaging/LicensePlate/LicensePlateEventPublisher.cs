using System;
using System.Collections.Generic;

namespace RemoteImaging.LicensePlate
{
    public class LicensePlateEventPublisher : ILicensePlateEventPublisher
    {
        public void RegisterForLicensePlateEvent(ILicensePlateObserver licensePlateObserver)
        {
            _observers.Add(licensePlateObserver);
        }

        public void PublishLicensePlate(LicensePlateInfo licensePlateInfo)
        {
            _observers.ForEach(o => o.LicensePlateCaptured(licensePlateInfo));
        }


        readonly List<ILicensePlateObserver> _observers = new List<ILicensePlateObserver>();
    }
}