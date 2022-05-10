using Creditot.Domain.Entities;
using Telegram.Bot.Types;

namespace Creditot.Services
{
    public interface IUserService
    {
        Task<Users> GetOrCreate(Update update);
    }
}
