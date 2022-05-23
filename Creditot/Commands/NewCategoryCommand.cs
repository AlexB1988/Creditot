using Creditot.Domain;
using Creditot.Domain.Entities;
using Creditot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Creditot.Commands
{
    public class NewCategoryCommand:BaseCommand
    {
        private readonly IUserService _userService;
        private readonly DataContext _dataContext;
        private readonly TelegramBotClient _telegramBotClient;
        public NewCategoryCommand(IUserService userService, DataContext dataContext, TelegramBot telegramBot)
        {
            _userService = userService;
            _dataContext=dataContext;
            _telegramBotClient = telegramBot.GetBot().Result;
        }
        public override string Name => CommandNames.NewCategoryCommand;
        public override async Task ExecuteAsync(Update update)
        {
            Console.WriteLine("Method NewCat");
            var user = await _userService.GetOrCreate(update);
            if (update.Message.Text!=null)
            {
                var category = new Categories
                {
                    Name = update.Message.Text,
                };
                await _dataContext.Categories.AddAsync(category);
                await _dataContext.SaveChangesAsync();

                var userCategory = new UsersCategories
                {
                    UsersId = user.Id,
                    CategoriesId = category.Id,
                };
                await _dataContext.UsersCategories.AddAsync(userCategory);
                await _dataContext.SaveChangesAsync();

                InlineKeyboardMarkup inlineKeyboard = new(
                    new[]
                      {
                    new[]
                    {
                    InlineKeyboardButton.WithCallbackData("Создать категорию", CommandNames.AddCategoryCommand),
                    InlineKeyboardButton.WithCallbackData("Выбрать категорию", CommandNames.GetCategoriesCommand)
                    },
                    new[]
                    {
                    InlineKeyboardButton.WithCallbackData("Получить статистику за день",CommandNames.GetDayRangeCommand)
                    },
                    new[]
                    {
                    InlineKeyboardButton.WithCallbackData("Получить статистику за неделю",CommandNames.GetWeekRangeCommand)
                    },
                    new[]
                    {
                    InlineKeyboardButton.WithCallbackData("Получить статистику за месяц",CommandNames.GetMonthRangeCommand)
                    }
                        });
                string text = "Отлично! Категория добавлена,\n" +
                            " теперь Вы можете ее выбрать,\n" +
                            " нажав на кнопку \"Выбрать категорию\"";
                await _telegramBotClient.SendTextMessageAsync(user.ChatId, text, replyMarkup:inlineKeyboard);
            }
        }
    }
}
