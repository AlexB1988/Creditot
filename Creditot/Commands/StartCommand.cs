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
                    InlineKeyboardButton.WithCallbackData("Создать категорию", CommandNames.AddCategoryCommand),
                    InlineKeyboardButton.WithCallbackData("Выбрать категорию", CommandNames.GetCategoriesCommand)
                    },
                    new[]
                    {
                    InlineKeyboardButton.WithCallbackData("Получить статистику за день",CommandNames.GetDayRangeCommand)
                    },
                    new[]
                    {
                    InlineKeyboardButton.WithCallbackData("Получить статистику за неделю",CommandNames.GetWeekRangeCommand)
                    },
                    new[]
                    {
                    InlineKeyboardButton.WithCallbackData("Получить статистику за месяц",CommandNames.GetMonthRangeCommand)
                    }
                    }) ;
            string text = "Привет! С моей помошью ты сможешь вести \n" +
                "учет своих расходов! Чтобы добавить первый расход,\n" +
                "можешь создать категорию,\n" +
                "либо выбрать из существующих!";
            await _telegramBotClient.SendTextMessageAsync(user.ChatId,text,replyMarkup:inlineKeyboard);
        }
    }
}
