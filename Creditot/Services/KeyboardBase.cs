
using Creditot.Domain.Entities;
using System.Collections;
using Telegram.Bot.Types.ReplyMarkups;

namespace Creditot.Services
{
    public abstract class KeyboardBase
    {
        public abstract List<InlineKeyboardButton[]> GetKeyboard(Users user);
    }
}
