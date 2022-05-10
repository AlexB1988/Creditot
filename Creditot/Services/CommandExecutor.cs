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
                    case CommandNames.GetCategoriesCommand:
                        await ExecuteCommand(CommandNames.GetCategoriesCommand, update);
                        return;
                    case CommandNames.AddCategoryCommand:
                        await ExecuteCommand(CommandNames.AddCategoryCommand,update);
                        return;
                }
            }

            if (update.Message is not null && update.Message.Text.Contains(CommandNames.StartCommand))
            {
                Console.WriteLine("/start");
                await ExecuteCommand(CommandNames.StartCommand, update);
                return;
            }

        }
        private async Task ExecuteCommand(string commandName, Update update)
        {
            _lastCommand = _commands.First(x=> x.Name == commandName);
            await _lastCommand.ExecuteAsync(update);
        }
    }
}
