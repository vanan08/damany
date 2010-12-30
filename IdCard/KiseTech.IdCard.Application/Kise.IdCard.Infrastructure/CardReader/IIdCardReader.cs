using System.Threading.Tasks;
using Kise.IdCard.Messaging;

namespace Kise.IdCard.Infrastructure.CardReader
{
    public interface IIdCardReader
    {
        Task<IdInfo> ReadAsync();
    }
}