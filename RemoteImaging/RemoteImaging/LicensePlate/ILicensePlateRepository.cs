using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public interface ILicensePlateRepository
    {
        void Save(LicensePlateInfo licensePlateInfo);

        IEnumerable<LicensePlateInfo> GetLicensePlates(Damany.Util.DateTimeRange dateTimeRange);
        IEnumerable<LicensePlateInfo> GetLicensePlates(string licensePlateNumber);
    }
}
