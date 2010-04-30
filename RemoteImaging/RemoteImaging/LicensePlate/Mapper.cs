using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public static class Mapper
    {
        static Mapper()
        {
            AutoMapper.Mapper.CreateMap<LicensePlateInfo, DtoLicensePlateInfo>();
            
        }

        public static DtoLicensePlateInfo ToDto(this LicensePlateInfo licensePlateInfo)
        {
            return null;
            
        }

        public static LicensePlateInfo ToBusinessObject(this DtoLicensePlateInfo dtoLicensePlateInfo)
        {
            return null;
        }


    }
}
