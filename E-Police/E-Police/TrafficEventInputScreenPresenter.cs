using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace E_Police
{
    using DataAccessLayer;

    public class TrafficEventInputScreenPresenter : ITrafficEventInputScreenObserver
    {

        private ITrafficEventInputScreen screen;
        private IDataProvider db;

        /// <summary>
        /// Initializes a new instance of the TrafficEventInputScreenPresenter class.
        /// </summary>
        /// <param name="screen"></param>
        public TrafficEventInputScreenPresenter(
            ITrafficEventInputScreen screen,
            IDataProvider db)
        {
            this.screen = screen;
            this.db = db;
        }

        public void Start()
        {
            this.screen.AttachPresenter(this);
        }


        private DTO.VehicleOwner CreateOwnerFromScreen()
        {
            DTO.VehicleOwner owner = new DTO.VehicleOwner()
            {
                Address = screen.OwnerAddr,
                Name = screen.OwnerName,
                PhoneNumber = screen.OwnerPhone,
            };

            return owner;
        }

        private DTO.Vehicle CreateVehicleFromScreen()
        {
            DTO.Vehicle vehicle = new DTO.Vehicle()
            {
                Category = screen.VehicleCategory,
                LicenseAreaCode = screen.LicensePlateNumber.Substring(0, 2),
                LicenseNumber = screen.LicensePlateNumber.Substring(2),
            };

            return vehicle;
        }

        private DTO.TrafficLawViolationEvent CreateEventFromScreen()
        {
            DTO.TrafficLawViolationEvent evt = new DTO.TrafficLawViolationEvent()
            {
                Time = DateTime.Parse(screen.EventTime),
            };

            return evt;
        }



        #region ITrafficEventInputScreenObserver Members

        public void LicensePlateNumberChanged()
        {
            throw new NotImplementedException();
        }

        public void SaveClicked()
        {
            throw new NotImplementedException();
        }

        public void SaveAndPrintClicked()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
