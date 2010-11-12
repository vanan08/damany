using System.Threading.Tasks;

namespace Kise.IdCard.Infrastructure.Sms
{
    public interface ITransport
    {
        void Start();
        Task<string> QueryAsync(string destination, string message);
    }
}