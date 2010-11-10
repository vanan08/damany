using System;
using System.Threading.Tasks;

namespace Kise.IdCard.Infrastructure.Sms
{
    public interface ISmsService
    {
        Task<string> QueryAsync(string destinationNumber, string message);
    }
}