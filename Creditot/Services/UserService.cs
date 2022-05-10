using Creditot.Domain;
using Creditot.Domain.Entities;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Microsoft.EntityFrameworkCore;

namespace Creditot.Services
{
    public class UserService:IUserService
    {
        private readonly DataContext _dataContext;

        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Users> GetOrCreate(Update update)
        {
            var user = update.Type switch
            {
                UpdateType.CallbackQuery => new Users
                {
                    UserName = update.CallbackQuery.From.Username,
                    ChatId = update.CallbackQuery.Message.Chat.Id
                },
                UpdateType.Message => new Users
                {
                    UserName = update.Message.Chat.Username,
                    ChatId = update.Message.Chat.Id
                }
            };
            var newUser =await _dataContext.Users.FirstOrDefaultAsync(x => x.ChatId == user.ChatId);
            if (newUser is not null)
                return newUser;
            var result = await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();

            return result.Entity;
        }
    }
}
