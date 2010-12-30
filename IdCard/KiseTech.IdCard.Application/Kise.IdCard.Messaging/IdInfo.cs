namespace Kise.IdCard.Messaging
{
    public class IdInfo
    {
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Minority { get; set; }
        public string BornDate { get; set; }
        public string Address { get; set; }
        public string IdCardNo { get; set; }
        public string GrantDept { get; set; }
        public string ValidateFrom { get; set; }
        public string ValidateUntil { get; set; }
        public byte[] PhotoData { get; set; }

    }
}