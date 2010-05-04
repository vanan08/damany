using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public interface ILicensePlateDataProvider
    {
        void Save(DtoLicensePlateInfo licensePlateInfo);

        IEnumerable<DtoLicensePlateInfo> GetLicensePlatesBetween(int cameraId, Damany.Util.DateTimeRange dateTimeRange);
        IEnumerable<DtoLicensePlateInfo> GetRecordsFor(string licensePlateNumber);
        IEnumerable<DtoLicensePlateInfo> GetLicensePlates(Predicate<DtoLicensePlateInfo> predicate);
    }
}
