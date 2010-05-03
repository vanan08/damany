using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public interface ILicensePlateDataProvider
    {
        void Save(DtoLicensePlateInfo licensePlateInfo);

        IEnumerable<DtoLicensePlateInfo> GetLicensePlates(int cameraId, Damany.Util.DateTimeRange dateTimeRange);
        IEnumerable<DtoLicensePlateInfo> GetLicensePlates(string licensePlateNumber);
    }
}
