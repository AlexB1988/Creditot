using Creditot.Domain;
using Creditot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Creditot.Commands
{
    public class DeleteStaticticsCommand:BaseCommand
    {
        DataContext _dataContext;
        TelegramBotClient _telegramBotClient;

        public DeleteStaticticsCommand(DataContext dataContext, TelegramBot telegramBot)
        {
            _dataContext = dataContext;
            _telegramBotClient = telegramBot.GetBot().Result;
        }

        public override string Name => CommandNames.DeleteStaticticsCommand;

        public override async Task ExecuteAsync (Update update)
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
            var chatId = update.CallbackQuery.Message.Chat.Id;

            var credits = _dataContext.Credits.Where(p => p.ChatId == chatId && p.IsDeleted==false).ToList();

            if (credits == null)
            {
                var text = "Нет данных для удаления";
                await _telegramBotClient.SendTextMessageAsync(chatId, text, replyMarkup: inlineKeyboard);
            }

            else
            {
              foreach(var c in credits)
                {
                    c.IsDeleted = true;
                    _dataContext.Update(c);
                }
                _dataContext.SaveChangesAsync();
                string text = $"Статистика удалена ❌.\n " +
                              $"Чтобы восстановить её\n" +
                              $"напишите @alexsnake999";
                await _telegramBotClient.SendTextMessageAsync(chatId, text,replyMarkup:inlineKeyboard);
            }

            //if (credits is not null)
            //{
            //    _dataContext.Credits.RemoveRange(credits);
            //    _dataContext.SaveChangesAsync();
            //    string text = $"Статистика удалена. " +
            //                  $"Чтобы восстановить её" +
            //                  $"напишите @alexsnake999";
            //    await _telegramBotClient.SendTextMessageAsync(chatId, text);
            //}
            //else
            //{
            //    var text = "Нет данных для удаления"; 
            //    await _telegramBotClient.SendTextMessageAsync(chatId, text);
            //}
        }
    }
}
