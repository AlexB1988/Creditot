using Creditot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Creditot.Commands
{
    public class AddCategoryCreditCommand:BaseCommand
    {
        private readonly IUserService _userService;
        private readonly TelegramBotClient _telegramBotClient;

        public AddCategoryCreditCommand(IUserService userService, TelegramBot telegramBot)
        {
            _userService = userService;
            _telegramBotClient = telegramBot.GetBot().Result;
        }
        public override string Name => CommandNames.AddCategoryCreditCommand;

        public override async Task ExecuteAsync(Update update)
        {
            Console.WriteLine($"{update.CallbackQuery.Data}=======================");
            //var user = _userService.GetOrCreate(update);
            string categoryName = update.CallbackQuery.Data;
            string text = $"Введите сумму расхода для категории \"{categoryName}\"";

            await _telegramBotClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, text);
        }
    }
}
