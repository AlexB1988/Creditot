using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Creditot.Commands
{
    public class SupportCommand:BaseCommand
    {
        private readonly TelegramBotClient _telegramBotClient;

        public SupportCommand(TelegramBot telegramBot)
        {
            _telegramBotClient = telegramBot.GetBot().Result;
        }

        public override string Name => CommandNames.SupportCommand;


        public override async Task ExecuteAsync(Update update)
        {

            InlineKeyboardMarkup inlineKeyboard = new(
                new[]
                  {
                    new[]
                    {
                    InlineKeyboardButton.WithCallbackData("Создать категорию ✅", CommandNames.AddCategoryCommand),
                    InlineKeyboardButton.WithCallbackData("Выбрать категорию 🕹", CommandNames.GetCategoriesCommand)
                    },
                    new[]
                    {
                    InlineKeyboardButton.WithCallbackData("Получить статистику за день 📊",CommandNames.GetDayRangeCommand)
                    },
                    new[]
                    {
                    InlineKeyboardButton.WithCallbackData("Получить статистику за неделю 📊",CommandNames.GetWeekRangeCommand)
                    },
                    new[]
                    {
                    InlineKeyboardButton.WithCallbackData("Получить статистику за месяц 📊",CommandNames.GetMonthRangeCommand)
                    },
                    new[]
                    {
                    InlineKeyboardButton.WithCallbackData("Удалить статистику ❌❌❌",CommandNames.DeleteStaticticsCommand)
                    },
                    new[]
                    {
                    InlineKeyboardButton.WithCallbackData("Обратиться в поддержку 📞",CommandNames.SupportCommand)
                    }
                 });

            string message = $"По всем вопросам, проблемам\n" +
                               $" и обращениям пишите @alexsnake999";

            long chatId = update.CallbackQuery.Message.Chat.Id;

            await _telegramBotClient.SendTextMessageAsync(chatId,message,replyMarkup:inlineKeyboard);
        }

    }
}
