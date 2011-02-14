namespace Damany.Imaging.Common
{
    public class PersonOfInterestDetectionResult
    {
        public PersonOfInterest Details { get; set; }
        public Common.Portrait Portrait { get; set; }
        public float Similarity { get; set; }
    }
}
