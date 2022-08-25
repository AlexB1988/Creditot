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
                //switch (_lastCommand?.Name)
                //{
                //    case CommandNames.AdminSendMessagesCommand:
                //        {
                //            Console.WriteLine("FSM");
                //            await ExecuteCommand(CommandNames.MailingListCommand, update);
                //            break;
                //        }
                //}


                switch (update.CallbackQuery.Data)
                {
                    case CommandNames.AddCategoryCommand:
                        await ExecuteCommand(CommandNames.AddCategoryCommand, update);
                        return;
                    case CommandNames.AdminSendMessagesCommand:
                        await ExecuteCommand(CommandNames.AdminSendMessagesCommand, update);
                        return;
                    case CommandNames.GetCategoriesCommand:
                        await ExecuteCommand(CommandNames.GetCategoriesCommand, update);
                        return;
                    case CommandNames.GetDayRangeCommand or CommandNames.GetWeekRangeCommand or CommandNames.GetMonthRangeCommand:
                        await ExecuteCommand(CommandNames.GetDayRangeCommand, update);
                        return;
                    case CommandNames.SupportCommand:
                        await ExecuteCommand(CommandNames.SupportCommand, update);
                        return;
                    case CommandNames.DeleteStaticticsCommand:
                        await ExecuteCommand(CommandNames.DeleteStaticticsCommand, update);
                        return;
                    default:
                        await ExecuteCommand(CommandNames.AddCategoryCreditCommand, update);
                        return;
                }
            }

            if (update.Message is not null && update.Message.Text.Contains(CommandNames.StartCommand))
            {
                await ExecuteCommand(CommandNames.StartCommand, update);
                return;
            }



            if (update.Message is not null && Convert.ToString(update.Message.Text).Length > 50)
            {
                await ExecuteCommand(CommandNames.MailingListCommand, update);
            }
            else if (update.Message is not null && double.TryParse(update.Message.Text, out var number) == true)
            {
                await ExecuteCommand(CommandNames.FinishCreditCommand, update);
            }

            else if (update.Message is not null)                                    //Не забыть потом переписать, 
            {                                                                  //_lastCommand пустой
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
