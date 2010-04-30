using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public interface ILicensePlateRepository
    {
        void Save(DtoLicensePlateInfo licensePlateInfo);

        IEnumerable<DtoLicensePlateInfo> GetLicensePlates(Damany.Util.DateTimeRange dateTimeRange);
        IEnumerable<DtoLicensePlateInfo> GetLicensePlates(string licensePlateNumber);
    }
}
