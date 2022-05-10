using Creditot.Domain;
using Creditot.Domain.Entities;
using Creditot.Services;
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
        private readonly ICommandExecutor _commandExecutor;
        public TelegramBotController(ICommandExecutor commandExecutor)
        {
          _commandExecutor=commandExecutor; 
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] object update)
        //public async Task<IActionResult> Update(Update update)
        {
            Console.WriteLine("OOOOPPPPssss");
            var upd = JsonConvert.DeserializeObject<Update>(update.ToString());

            if (upd?.Message?.Text is null && upd?.CallbackQuery is null)
                return Ok();

            try
            {
               await _commandExecutor.Execute(upd);
            }
            catch (Exception ex)
            {
                return Ok();
            }
            return Ok();
        }
    }
}
