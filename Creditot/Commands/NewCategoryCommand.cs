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
                    },
                    new[]
                    {
                    InlineKeyboardButton.WithCallbackData("Удалить статистику ❌❌❌",CommandNames.DeleteStaticticsCommand)
                    },
                    new[]
                    {
                    InlineKeyboardButton.WithCallbackData("Обратиться в поддержку 📞",CommandNames.SupportCommand)
                    }
                });
            var user = await _userService.GetOrCreate(update);

            var catName = update.Message.Text.ToLower();
            Console.WriteLine(catName);

            var oldCat=_dataContext.Categories.FirstOrDefault(p => p.Name.ToLower() == catName);


            if (oldCat is null)
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

                string text = "Отлично! Категория добавлена,\n" +
                                " теперь Ты можешь ее выбрать,\n" +
                                " нажав на кнопку \"Выбрать категорию\"";
                await _telegramBotClient.SendTextMessageAsync(user.ChatId, text, replyMarkup: inlineKeyboard);
            }

            var oldUsersCat = _dataContext.UsersCategories.FirstOrDefault(p => p.UsersId == user.Id && p.CategoriesId==oldCat.Id);

            Console.WriteLine($"{oldUsersCat}<=<=<=<=");
           


            if (oldUsersCat!=null)
            {
                string text = "Данная категория уже добавлена,\n" +
                                " ты можешь ее выбрать,\n" +
                                " нажав на кнопку \"Выбрать категорию\"";
                await _telegramBotClient.SendTextMessageAsync(user.ChatId, text, replyMarkup: inlineKeyboard);
            }

            else if (oldCat!=null)
            {
                var userCategory = new UsersCategories
                {
                    UsersId = user.Id,
                    CategoriesId = oldCat.Id,
                };
                await _dataContext.UsersCategories.AddAsync(userCategory);
                await _dataContext.SaveChangesAsync();

                string text = "Отлично! Категория добавлена,\n" +
                                " теперь Ты можешь ее выбрать,\n" +
                                " нажав на кнопку \"Выбрать категорию\"";
                await _telegramBotClient.SendTextMessageAsync(user.ChatId, text, replyMarkup: inlineKeyboard);
            }


        }
    }
}
