using Creditot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Creditot.Commands
{
    public class AddCategoryCommand:BaseCommand
    {
        private readonly IUserService _userService;
        private readonly TelegramBotClient _telegramBotClient;

        public AddCategoryCommand(IUserService userService, TelegramBot telegramBot)
        {
            _userService=userService;
            _telegramBotClient = telegramBot.GetBot().Result;
        }

        public override string Name => CommandNames.AddCategoryCommand;

        public override async Task ExecuteAsync(Update update)
        {
            var user = await _userService.GetOrCreate(update);
            Console.WriteLine(Name);
            string text = $"Введите название категории:\n" +
                            $"(для перехода в главное меню нажмите /start)";
            await _telegramBotClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, text);


        }

    }
}
