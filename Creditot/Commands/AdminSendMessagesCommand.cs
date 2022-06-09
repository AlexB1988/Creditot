using Creditot.Domain;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Creditot.Commands
{
    public class AdminSendMessagesCommand:BaseCommand
    {
        private readonly DataContext _dataContext;
        private readonly TelegramBotClient _telegramBotClient;
        public AdminSendMessagesCommand(DataContext dataContext, TelegramBot telegramBot)
        {
            _dataContext = dataContext;
            _telegramBotClient = telegramBot.GetBot().Result;
        }

        public override string Name => CommandNames.AdminSendMessagesCommand;

        
        public override async Task ExecuteAsync(Update update)
        {

            var chatId =update.CallbackQuery.Message.Chat.Id;

            string text = $"Напишите текст рассылки: ";

            await _telegramBotClient.SendTextMessageAsync(chatId, text);

        }
    }
}
