using Creditot.Commands;
using System.Linq;
using Creditot.Domain;
using Creditot.Domain.Entities;
using System.Collections;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Microsoft.EntityFrameworkCore;

namespace Creditot.Services
{
    public class CategoriesKeyboard:KeyboardBase
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public CategoriesKeyboard(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        public override List<InlineKeyboardButton[]> GetKeyboard(Users user)
        {
            List<InlineKeyboardButton[]> keyboard =new List<InlineKeyboardButton[]>();
            var categories = _dataContext.UsersCategories.Include(p => p.Categories)
                .Where(p=>p.UsersId==user.Id).ToList();
            //int i=categories.Count();
            if (categories is not null)
            {
                for (int m = 0; m < categories.Count; m++)
                {
                        keyboard.Add(
                            new[]
                            {
                    InlineKeyboardButton.WithCallbackData(categories[m].Categories.Name, categories[m].Id.ToString())
                            });
                }
            }
            return keyboard;
        }
    }
}
