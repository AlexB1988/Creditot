using Creditot.Domain.Entities;

namespace Creditot.Services
{
    public interface IOperationService
    {
        Task<Credits> GetLast(long chatId);
    }
}
