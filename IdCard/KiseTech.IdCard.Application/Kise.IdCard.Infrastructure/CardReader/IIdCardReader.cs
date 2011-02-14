using System.Threading.Tasks;

namespace Kise.IdCard.Infrastructure.CardReader
{
    public interface IIdCardReader
    {
        Task<Model.IdCardInfo> ReadAsync();
    }
}