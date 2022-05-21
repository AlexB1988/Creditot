using Creditot.Domain;
using Creditot.Domain.Entities;
using Creditot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Creditot.Commands
{
    public class AddCategoryCreditCommand:BaseCommand
    {
        //private readonly IUserService _userService;
        private readonly TelegramBotClient _telegramBotClient;
        private readonly DataContext _dataContext;
        public AddCategoryCreditCommand(TelegramBot telegramBot, DataContext dataContext)
        {
            //_userService = userService;
            _telegramBotClient = telegramBot.GetBot().Result;
            _dataContext = dataContext;
        }
        public override string Name => CommandNames.AddCategoryCreditCommand;

        public override async Task ExecuteAsync(Update update)
        {
            int i = Convert.ToInt32(update.CallbackQuery.Data);

            var categories = _dataContext.UsersCategories.FirstOrDefault(x => x.Id == i);
            var categoryName = _dataContext.Categories.FirstOrDefault(x => x.Id == categories.Id);

            var credit =new Credits
                { 
                   ChatId=update.CallbackQuery.Message.Chat.Id,
                   UsersCategoriesId=i,
                };
            await _dataContext.Credits.AddAsync(credit);
            await _dataContext.SaveChangesAsync();
            string text = $"Введите сумму расхода для категории \"{categoryName.Name}\" \n" +
                          $"Вводите только цифры!";

            await _telegramBotClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, text);
        }
    }
}
