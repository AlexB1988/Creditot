using Creditot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.InlineQueryResults;

namespace Creditot.Commands
{
    public class StartCommand:BaseCommand
    {
        private readonly IUserService _userService;
        private readonly TelegramBotClient _telegramBotClient;

        public StartCommand(IUserService userService, TelegramBot telegramBot)
        {
            _userService = userService;
            _telegramBotClient = telegramBot.GetBot().Result;
        }

        public override string Name => CommandNames.StartCommand;

        public override async Task ExecuteAsync (Update update)
        {
            var user = await _userService.GetOrCreate(update);

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
                    }) ;
            InlineKeyboardMarkup inlineKeyboardAdmin = new(
            new[]
              {
                    new[]
                    {
                    InlineKeyboardButton.WithCallbackData("Создать категорию ✅", CommandNames.AddCategoryCommand),
                    InlineKeyboardButton.WithCallbackData("Выбрать категорию  🕹", CommandNames.GetCategoriesCommand)
                    },
                    new[]
                    {
                    InlineKeyboardButton.WithCallbackData("Разослать сообщения",CommandNames.AdminSendMessagesCommand)
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
                    }
                });
            string text = $"Привет👋👋👋 \n" +
                            "Я буду вести учёт\n" +
                            "твоих расходов 📝📝📝 \n" +
                            "по всем вопросам и предложениям " +
                            "обращайтесь к @alexsnake999\n" +
                            "Также следите за обновлениями и\n" +
                            "оставляйте комментарии на канале\n" +
                            "https://t.me/creditbotchannel \n" +
                            "Чтобы добавить свой первый расход, создай категорию\n" +
                            " расходов, либо выбери одну из уже существующих\n" +
                            "👇👇👇👇👇👇👇👇👇👇👇";
            if (update.Message.Chat.Id == 851824368)
            {
                await _telegramBotClient.SendTextMessageAsync(user.ChatId, text, replyMarkup: inlineKeyboardAdmin);
            }
            else
            {
                await _telegramBotClient.SendTextMessageAsync(user.ChatId, text, replyMarkup: inlineKeyboard);
            }  
        }
    }
}
