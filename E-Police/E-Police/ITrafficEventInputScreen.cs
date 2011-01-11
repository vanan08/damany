using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace E_Police
{
    public interface ITrafficEventInputScreen
    {
        string EventTime { get; set; }
        string CapturedAt { get; set; }
        string EventDescription { get; set; }
        string LicensePlateCategory { get; set; }
        string VehicleCategory { get; set; }
        string LicensePlateNumber { get; set; }
        string OwnerName { get; set; }
        string OwnerAddr { get; set; }
        string OwnerPhone { get; set; }

        Image EvidencePic { get; set; }

        void AttachPresenter(ITrafficEventInputScreenObserver presenter);

    }
}
