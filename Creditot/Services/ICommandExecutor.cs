using Telegram.Bot.Types;

namespace Creditot.Services
{
    public interface ICommandExecutor
    {
         Task Execute(Update update);
    }
}
