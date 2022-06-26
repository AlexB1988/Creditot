using Creditot.Domain;
using Creditot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Creditot.Commands
{
    public class FinishCreditCommand:BaseCommand
    {
        private readonly IUserService _userService;
        private readonly DataContext _dataContext;
        private readonly TelegramBotClient _telegramBotClient;
        private readonly IOperationService _operationService;

        public FinishCreditCommand(IOperationService operationService,IUserService userService,DataContext dataContext, TelegramBot telegramBot)
        {
            _operationService = operationService;
            _dataContext = dataContext;
            _userService = userService;
            _telegramBotClient = telegramBot.GetBot().Result;
        }

        public override string Name => CommandNames.FinishCreditCommand;

        public override async Task ExecuteAsync(Update update)
        {
            double credit = double.Parse(update.Message.Text);
            long chatId = update.Message.Chat.Id;

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

            var lastCredit =await  _operationService.GetLast(chatId);
            lastCredit.Sum = credit;

            await _dataContext.SaveChangesAsync();

            string text = "Расход успешно добавлен❕❕❕\n" +
                            "Ты можешь посмотреть\n" +
                            "📊статистику 📊\n" +
                            "своих расходов, нажав на одну\n" +
                            "из кнопок🕹🕹🕹 ниже";

            await _telegramBotClient.SendTextMessageAsync(chatId, text,replyMarkup:inlineKeyboard);
        }
    }
}
