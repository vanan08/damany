namespace Kise.IdCard.Infrastructure
{
    public interface IMinorityDictionary
    {
        string this[int code] { get; }
    }
}