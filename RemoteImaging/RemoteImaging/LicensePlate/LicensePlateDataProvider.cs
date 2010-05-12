using System;
using System.Collections.Generic;
using Damany.Util;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;

namespace RemoteImaging.LicensePlate
{
    public class LicensePlateDataProvider : ILicensePlateDataProvider
    {
        private readonly string _outputDirectory;
        private readonly IEmbeddedObjectContainer _db4oContainer;

        public LicensePlateDataProvider(string outputDirectory)
        {
            if (string.IsNullOrEmpty(outputDirectory))
                throw new ArgumentNullException("outputDirectory");

            if (!System.IO.Directory.Exists(outputDirectory))
                throw new System.IO.DirectoryNotFoundException("output directory of licenseplate not found");

            _outputDirectory = outputDirectory;

            var dbFilePath = System.IO.Path.Combine(_outputDirectory, "LicensePlates.db4o");
            _db4oContainer = Db4oEmbedded.OpenFile(dbFilePath);

        }


        public void Save(DtoLicensePlateInfo licensePlateInfo)
        {
            _db4oContainer.Store(licensePlateInfo);
            _db4oContainer.Commit();
        }


        public IEnumerable<DtoLicensePlateInfo> GetLicensePlatesBetween(int cameraId, DateTimeRange dateTimeRange)
        {
            return _db4oContainer.Query<DtoLicensePlateInfo>(l =>
                                                                 {

                                                                     var match = l.CaptureTime >= dateTimeRange.From &&
                                                                                 l.CaptureTime <= dateTimeRange.To;

                                                                     if (cameraId != -1)
                                                                     {
                                                                         match = match && l.CapturedFrom == cameraId;
                                                                     }
                                                                                 ;
                                                                     return match;
                                                                 });
        }

        public IEnumerable<DtoLicensePlateInfo> GetRecordsFor(string licensePlateNumber)
        {
            return _db4oContainer.Query<DtoLicensePlateInfo>(l =>
                                                                 {
                                                                    return l.LicensePlateNumber.ToUpper().Contains(
                                                                             licensePlateNumber.ToUpper());
                                                                 });

        }

        public IEnumerable<DtoLicensePlateInfo> GetLicensePlates(Predicate<DtoLicensePlateInfo> predicate)
        {
            var query = from DtoLicensePlateInfo dto in _db4oContainer
                        where predicate(dto)
                        select dto;

            return query;
        }
    }
}