using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public class Mapper
    {
        private readonly string _outputDirectory;
        private const string LicensePlatesDirectoryName = "LicensePlates";

        public Mapper(string outputDirectory)
        {
            if (outputDirectory == null) throw new ArgumentNullException("outputDirectory");
            if (!System.IO.Directory.Exists(outputDirectory))
            {
                throw new System.IO.DirectoryNotFoundException("output directory for licenseplate not found");
            }

            _outputDirectory = outputDirectory;

            InitializeAutoMapper();
        }


        private void InitializeAutoMapper()
        {
            AutoMapper.Mapper.CreateMap<LicensePlateInfo, DtoLicensePlateInfo>()
                .ForMember("LicensePlateImageFileRelativePath",
                           opt => opt.MapFrom(licenseplateinfo => GetRelativeImagePath(licenseplateinfo)));

            AutoMapper.Mapper.CreateMap<DtoLicensePlateInfo, LicensePlateInfo>()
                .ForMember("LicensePlateImageFileAbsolutePath",
                           opt => opt.MapFrom(dto => BuildAbsoluteImagePath(dto.LicensePlateImageFileRelativePath)));

            AutoMapper.Mapper.AssertConfigurationIsValid();
        }

        public DtoLicensePlateInfo ToDto(LicensePlateInfo licensePlateInfo)
        {
            var dto = AutoMapper.Mapper.Map<LicensePlateInfo, DtoLicensePlateInfo>(licensePlateInfo);
            return dto;
        }

        public LicensePlateInfo ToBusinessObject(DtoLicensePlateInfo dtoLicensePlateInfo)
        {
            var biz = AutoMapper.Mapper.Map<DtoLicensePlateInfo, LicensePlateInfo>(dtoLicensePlateInfo);
            return biz;
        }


        private string GetRelativeImagePath(LicensePlateInfo licensePlateInfo)
        {
            var relativePath = string.Format(@"{0}\{1:d4}{2:d2}{3:d2}.jpg",
                                                LicensePlatesDirectoryName,
                                                licensePlateInfo.CaptureTime.Year, 
                                                licensePlateInfo.CaptureTime.Month,
                                                licensePlateInfo.CaptureTime.Day);

            return relativePath;
        }

        private string BuildAbsoluteImagePath(LicensePlateInfo licensePlateInfo)
        {
            var relative = GetRelativeImagePath(licensePlateInfo);

            var absolute = System.IO.Path.Combine(_outputDirectory, relative);

            return absolute;        
        }

        private string BuildAbsoluteImagePath(string relativePath)
        {
            var absolute = System.IO.Path.Combine(_outputDirectory, relativePath);

            return absolute;
        }


    }
}
