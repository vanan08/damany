using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public class LicensePlateRepository
    {
        private readonly ILicensePlateDataProvider _dataProvider;

        public LicensePlateRepository(ILicensePlateDataProvider dataProvider)
        {
            if (dataProvider == null) throw new ArgumentNullException("dataProvider");
            _dataProvider = dataProvider;
        }


        public void Save(LicensePlateInfo licensePlateInfo)
        {
            
        }
    }
}
