using System;
using System.Collections.Generic;
using Damany.Util;
using Db4objects.Db4o;

namespace RemoteImaging.LicensePlate
{
    public class LicensePlateRepository : ILicensePlateRepository
    {
        private readonly string _databaseFilePath;
        private IEmbeddedObjectContainer _db4oContainer;

        public LicensePlateRepository(string databaseFilePath)
        {
            if (string.IsNullOrEmpty(databaseFilePath))
            {
                throw new ArgumentNullException("databaseFilePath");
            }

            _databaseFilePath = databaseFilePath;
            _db4oContainer = Db4oEmbedded.OpenFile(databaseFilePath);

        }


        public void Save(DtoLicensePlateInfo licensePlateInfo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DtoLicensePlateInfo> GetLicensePlates(DateTimeRange dateTimeRange)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DtoLicensePlateInfo> GetLicensePlates(string licensePlateNumber)
        {
            throw new NotImplementedException();
        }
    }
}