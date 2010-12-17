using System;

namespace Kise.IdCard.Server
{
    public class IdSpec
    {
        public string Name { get; set; }
        public string SexCode { get; set; }
        public string IdNo { get; set; }
        public DateTime BornDate { get; set; }
        public byte[] ImageData { get; set; }
        public string Employer { get; set; }
        public string MinorityCode { get; set; }
    }
}
