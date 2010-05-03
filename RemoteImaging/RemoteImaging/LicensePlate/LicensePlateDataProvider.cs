using System;
using System.Collections.Generic;
using Damany.Util;
using Db4objects.Db4o;

namespace RemoteImaging.LicensePlate
{
    public class LicensePlateDataProvider : ILicensePlateDataProvider
    {
        private readonly string _outputDirectory;
        private IEmbeddedObjectContainer _db4oContainer;

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
        }

        public IEnumerable<DtoLicensePlateInfo> GetLicensePlates(int cameraId, DateTimeRange dateTimeRange)
        {
            return _db4oContainer.Query<DtoLicensePlateInfo>(l =>
                                                                 {
                                                                     var match = cameraId == l.CapturedFrom &&
                                                                                 l.CaptureTime >= dateTimeRange.From &&
                                                                                 l.CaptureTime <= dateTimeRange.To;
                                                                     return match;
                                                                 });
        }

        public IEnumerable<DtoLicensePlateInfo> GetLicensePlates(string licensePlateNumber)
        {
            return _db4oContainer.Query<DtoLicensePlateInfo>(l =>
                                                                 {
                                                                    return l.LicensePlateNumber.ToUpper().Contains(
                                                                             licensePlateNumber.ToUpper());
                                                                 });

        }
    }
}