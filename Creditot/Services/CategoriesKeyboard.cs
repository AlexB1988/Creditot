using Creditot.Commands;
using Creditot.Domain;
using System.Collections;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Creditot.Services
{
    public class CategoriesKeyboard:KeyboardBase
    {
        private readonly DataContext _dataContext;

        public CategoriesKeyboard(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public override List<InlineKeyboardButton> GetKeyboard()
        {
            List<InlineKeyboardButton> keyboard =new List<InlineKeyboardButton>();
            var categories = _dataContext.Categories.ToList();
            if (categories is not null)
            {
                for (int m = 0; m < categories.Count; m++)
                {
                    keyboard.Add(
                InlineKeyboardButton.WithCallbackData(categories[m].Name,categories[m].Name)
                      );
                }
            }
            return keyboard;
        }
    }
}
