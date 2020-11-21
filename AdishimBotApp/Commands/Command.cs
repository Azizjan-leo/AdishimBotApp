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

        public string RemoveCommand(string text)
        {
            foreach (var name in Names)
            {
                text = text.Replace(name, "", true, null);
            }
            if (text != string.Empty)
                return (text[0] == ' ') ? text.Remove(0, 1) : text;
            return string.Empty;
        }

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
