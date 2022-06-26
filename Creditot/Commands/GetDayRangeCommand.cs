using Creditot.Domain;
using Creditot.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Creditot.Commands
{
    public class GetDayRangeCommand:BaseCommand
    {
        private readonly DataContext _dataContext;
        private readonly TelegramBotClient _telegramBotClient;

        public GetDayRangeCommand(DataContext dataContext, TelegramBot telegramBot)
        {
            _dataContext = dataContext;
            _telegramBotClient = telegramBot.GetBot().Result;
        }

        public override string Name => CommandNames.GetDayRangeCommand;

        public override async Task ExecuteAsync(Update update)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;
            string daysRangeString = update.CallbackQuery.Data;
            var data = daysRangeString.Split(":");
            int daysRange = int.Parse(data[0]);

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
                    },

                    });

            var allCredits = from credits in _dataContext.Credits
                             join userCategories in _dataContext.UsersCategories on credits.UsersCategoriesId equals userCategories.Id
                             join categories in _dataContext.Categories on userCategories.CategoriesId equals categories.Id
                             where credits.ChatId == chatId
                             where credits.IsDeleted==false
                             where credits.CreatedAt < DateTime.UtcNow
                             where credits.CreatedAt >= DateTime.UtcNow.AddDays(-daysRange)
                             select new
                             {
                                 Name = categories.Name,
                                 Sum = credits.Sum,
                                 Chat = credits.ChatId
                             };
            var summaryCredits = allCredits.GroupBy(u => u.Name, u => u.Sum).Select(g => new
            {
                g.Key,
                Sum = g.Sum()
            });

            double? summary = allCredits.Where(u => u.Chat == chatId).Sum(u => u.Sum);

            var message = new StringBuilder("Ваши расходы по категориям:\n");
            foreach (var i in summaryCredits)
            {
                message.AppendLine($"{i.Key}:\t{i.Sum}");
            }
            message.AppendLine($"Итого:\t\t{summary} ");


            await _telegramBotClient.SendTextMessageAsync(chatId, message.ToString(),replyMarkup:inlineKeyboard);
        }
    }
}
