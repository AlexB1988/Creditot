using Creditot.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Creditot.Commands
{
    public class GetDayRangeCommand:BaseCommand
    {
        private readonly DataContext _dataContext;
        private readonly TelegramBotClient _telegramBotClient;

        public GetDayRangeCommand(DataContext dataContext, TelegramBot telegramBot)
        {
            _dataContext = dataContext;
            _telegramBotClient = telegramBot.GetBot().Result;
        }

        public override string Name => CommandNames.GetDayRangeCommand;

        public override async Task ExecuteAsync(Update update)
        {
            long chatId = update.CallbackQuery.Message.Chat.Id;
            string daysRangeString = update.CallbackQuery.Data;
            var data = daysRangeString.Split(":");
            int daysRange = int.Parse(data[0]);
            Console.WriteLine("------1");
            SqlParameter param1 = new("@chatId", chatId);
            SqlParameter param2 = new("@daysRange", daysRange);
            Console.WriteLine("------2");
            var summaryCredits = _dataContext.FromSqlRaw("GetDateRange @chatId,@daysRange",param1,param2).ToList();
            Console.WriteLine("------3");
            Console.WriteLine(summaryCredits);
            Console.WriteLine("-----4");

            var message = new StringBuilder("Ваши расходы за день по категориям:\n");
            //message.AppendLine(summaryCredits);
            //foreach (var i in summaryCredits)
            //{
            //    message.AppendLine($"{i.}");
            //}
        }
    }
}
