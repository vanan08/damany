using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public class LicensePlateRepository : ILicensePlateObserver
    {
        private readonly string _outputDirectory;
        private readonly ILicensePlateDataProvider _dataProvider;
        private readonly Mapper _mapper;



        public LicensePlateRepository(string outputDirectory, ILicensePlateDataProvider dataProvider)
        {
            if (outputDirectory == null) throw new ArgumentNullException("outputDirectory");
            if (!System.IO.Directory.Exists(outputDirectory))
            {
                throw new System.IO.DirectoryNotFoundException("output directory of license plate image does't exist");
            }
            if (dataProvider == null) throw new ArgumentNullException("dataProvider");

            _outputDirectory = outputDirectory;
            _dataProvider = dataProvider;
            _mapper = new Mapper(outputDirectory);
        }


        public void Save(LicensePlateInfo licensePlateInfo)
        {
            var dto = _mapper.ToDto(licensePlateInfo);

            var absolutePath = System.IO.Path.Combine(_outputDirectory, dto.LicensePlateImageFileRelativePath);

            var dir = System.IO.Directory.GetParent(absolutePath).ToString();
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }

            System.IO.File.WriteAllBytes(absolutePath, licensePlateInfo.ImageData);

            _dataProvider.Save(dto);
        }


        public IEnumerable<LicensePlateInfo> GetLicensePlates(SearchCretia searchCretia)
        {
            var dtos = _dataProvider.GetLicensePlates(searchCretia.MatchWith);
            return dtos.Select(dto => _mapper.ToBusinessObject(dto));
        }

        public IEnumerable<LicensePlateInfo> GetRecordsFor(string licensePlateNumber)
        {
            var dtos = _dataProvider.GetRecordsFor(licensePlateNumber);
            return dtos.Select(dto => _mapper.ToBusinessObject(dto));
        }


        public void LicensePlateCaptured(LicensePlateInfo licensePlateInfo)
        {
            Save(licensePlateInfo);
        }

        public ILicensePlateEventPublisher Publisher
        {
            set
            {
                value.RegisterForLicensePlateEvent(this);
            }
        }

    }
}
