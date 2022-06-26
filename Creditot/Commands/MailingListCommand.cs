using Creditot.Domain;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Creditot.Commands
{
    public class MailingListCommand:BaseCommand
    {
        private readonly DataContext _dataContext;
        private readonly TelegramBotClient _telegramBotClient;
        public MailingListCommand(DataContext dataContext, TelegramBot telegramBot)
        {
            _dataContext = dataContext;
            _telegramBotClient = telegramBot.GetBot().Result;
        }

        public override string Name => CommandNames.MailingListCommand;

        public override async Task ExecuteAsync(Update update)
        {
            var chatIdAdmin = update.Message.Chat.Id;
            var chatId = _dataContext.Users.ToList();

            string textTemp =update.Message.Text;
            string text = $"{textTemp}\n " +
                         $" Для перехода в главное меню нажмите\n" +
                         $"👉👉👉 /start 👈👈👈";

            if (chatIdAdmin == 851824368)
            {
                foreach (var c in chatId)
                {
                    await _telegramBotClient.SendTextMessageAsync(c.ChatId, text);
                }
            }
        }
    }
}
