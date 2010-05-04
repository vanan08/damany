using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{

    public class LicensePlateSearchPresenter : ILicensePlateSearchPresenter
    {
        private readonly ILicenseplateSearchScreen _screen;
        private readonly LicensePlateRepository _repository;

        public LicensePlateSearchPresenter(ILicenseplateSearchScreen screen,  LicensePlateRepository repository)
        {
            if (screen == null) throw new ArgumentNullException("screen");
            if (repository == null) throw new ArgumentNullException("repository");

            _screen = screen;
            _repository = repository;
        }


        public void Search()
        {
            var predicates = new SearchCretia();

            if (_screen.MatchLicenseNumber)
            {
                predicates.AddCretia(dto => dto.LicensePlateNumber.ToUpper().Contains(_screen.LicenseNumber.ToUpper()));
            }

            if (_screen.MatchTimeRange)
            {
                var range = _screen.Range;
                predicates.AddCretia(dto => dto.CaptureTime >= range.From && dto.CaptureTime <= range.To);
            }

            var query = _repository.GetLicensePlates(predicates);
            _screen.Clear();
            foreach (var licensePlateInfo in query)
            {
                _screen.AddLicensePlateInfo(licensePlateInfo);
            }
            
        }

        public void Start()
        {
            _screen.AttachPresenter(this);
            _screen.Show();
        }
    }
}
