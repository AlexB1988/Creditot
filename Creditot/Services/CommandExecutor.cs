using Creditot.Commands;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Creditot.Services
{
    public class CommandExecutor:ICommandExecutor
    {
        private readonly List<BaseCommand> _commands;
        private BaseCommand _lastCommand;

        public CommandExecutor(IServiceProvider serviceProvider)
        {
            _commands = serviceProvider.GetServices<BaseCommand>().ToList();
        }
        public async Task Execute(Update update)
        {
            if (update.Type==UpdateType.CallbackQuery)
            {
                Console.WriteLine("Callback");
                switch (update.CallbackQuery.Data)
                {
                    case CommandNames.AddCategoryCommand:
                        await ExecuteCommand(CommandNames.AddCategoryCommand, update);
                        return;

                    case CommandNames.GetCategoriesCommand:
                        await ExecuteCommand(CommandNames.GetCategoriesCommand, update);
                        return;
                    case CommandNames.GetDayRangeCommand:
                        await ExecuteCommand(CommandNames.GetDayRangeCommand, update);
                        return;
                    default:
                        await ExecuteCommand(CommandNames.AddCategoryCreditCommand, update);
                        return;
                }
            }

            if (update.Message is not null && update.Message.Text.Contains(CommandNames.StartCommand))
            {
                Console.WriteLine("/start");
                await ExecuteCommand(CommandNames.StartCommand, update);
                return;
            }
            if (update.Message is not null && double.TryParse(update.Message.Text,out var number)==true)
            {
                Console.WriteLine("FinishCommand");
                await ExecuteCommand(CommandNames.FinishCreditCommand, update);
            }
            if (_lastCommand?.Name is null)                                    //Не забыть потом переписать, 
            {                                                                  //_lastCommand пустой
                Console.WriteLine("NewCategory");
                await ExecuteCommand(CommandNames.NewCategoryCommand, update);
            }
            //switch (_lastCommand?.Name)
            //{
            //    case CommandNames.AddCategoryCommand:
            //        {
            //            Console.WriteLine("FSM");
            //            await ExecuteCommand(CommandNames.NewCategoryCommand, update);
            //            break;
            //        }
            //}
        }
        private async Task ExecuteCommand(string commandName, Update update)
        {
            _lastCommand = _commands.First(x=> x.Name == commandName);
            await _lastCommand.ExecuteAsync(update);
        }
    }
}
