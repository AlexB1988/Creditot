using Creditot.Domain;
using Creditot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Creditot.Commands
{
    public class GetCategoriesCommand:BaseCommand
    {
        private readonly TelegramBotClient _telegramBotClient;
        private readonly KeyboardBase _keyboard;
        private readonly IUserService _userService;

        public GetCategoriesCommand(TelegramBot telegramBot,KeyboardBase keyboard, IUserService userService)
        {
            _telegramBotClient = telegramBot.GetBot().Result;
            _keyboard= keyboard;
            _userService= userService;
        }
        public override string Name => CommandNames.GetCategoriesCommand;
        public override async Task ExecuteAsync(Update update)
        {
            var user= await _userService.GetOrCreate(update); 
            InlineKeyboardMarkup inlineKeyboard = new(

                _keyboard.GetKeyboard(user)        //Не забыть переделать, клавиатура горизонтальная (нужно вертикальную)
            ) ;
            string text = "Здесь Вы можете выбрать категорию\n" +
                          "своего расхода. Просто нажмите\n" +
                          " на подходящую из списка!";
            await _telegramBotClient.SendTextMessageAsync(user.ChatId,text, replyMarkup: inlineKeyboard);
        }
    }
}
