using Creditot.Domain;
using Creditot.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Creditot.Controllers
{
    [ApiController]
    [Route("api/message/update")]
    public class TelegramBotController:Controller
    {
        private readonly TelegramBotClient _telegramBotClient;
        private readonly DataContext _context;
        public TelegramBotController(TelegramBot telegramBot, DataContext context)
        {
            _context = context;
            _telegramBotClient = telegramBot.GetBot().Result;
            Console.WriteLine(_telegramBotClient is null); 
        }

        [HttpPost]
        //public async Task<IActionResult> Update([FromBody] object update)
        public async Task<IActionResult> Update(Update update)
        {
            Console.WriteLine("OOOOPPPPssss");
            //var upd = JsonConvert.DeserializeObject<Update>(update.ToString());
            //var chat = upd.Message?.Chat;
            var chat = update.Message.Chat;
            if (chat == null)
            {
                return Ok();
            }
            Console.WriteLine(chat);
            var appUser = new AppUser
            {
                Username = chat.Username,
                ChatId = chat.Id
            };

            await _context.Users.AddAsync(appUser);
            await _context.SaveChangesAsync();

            await _telegramBotClient.SendTextMessageAsync(chat.Id, "You've been registered");
            return Ok();
        }
    }
}
