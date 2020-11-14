using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AdishimBotApp.Commands
{
    public abstract class Command
    {
        public abstract List<string> Names { get; }

        public abstract Task Execute(Message message, TelegramBotClient client);

        public bool Contains (string command)
        {
            foreach (var name in Names)
            {
                if(command.Contains(name.ToLower())) // && command.Contains(AppSettings.Name);
                {
                    return true;
                }
            }
            return false;
        }
    }
}
