
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Creditot
{
    public class TelegramBot
    {
        private readonly IConfiguration _configuration;

        public TelegramBot(IConfiguration configuration)`
        {
            _configuration = configuration;
            Console.WriteLine(_configuration["Url"]);
        }

        public async Task<TelegramBotClient> GetBot()
        {
            var telegramBot = new TelegramBotClient(_configuration["TOKEN"]);

            var hook = $"{_configuration["Url"]}api/massege/update";

            await telegramBot.SetWebhookAsync(hook);
            Console.WriteLine(hook);
            return telegramBot;
        }
    }
}
