
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Creditot
{
    public class TelegramBot
    {
        private readonly IConfiguration _configuration;
        private TelegramBotClient _telegramBotClient;

        public TelegramBot(IConfiguration configuration)
        {
            _configuration = configuration;
            Console.WriteLine(_configuration["Url"]);
        }

        public async Task<TelegramBotClient> GetBot()
        {
            if (_telegramBotClient is not null)
            {
                return _telegramBotClient;
            }
            _telegramBotClient = new TelegramBotClient(_configuration["TOKEN2"]);

            var hook = $"{_configuration["Url"]}/api/message/update";

            await _telegramBotClient.SetWebhookAsync(hook);
            Console.WriteLine(hook);
            return _telegramBotClient;
        }
    }
}
