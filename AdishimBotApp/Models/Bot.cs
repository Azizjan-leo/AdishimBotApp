using AdishimBotApp.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;

namespace AdishimBotApp.Models
{
    public static class BotCommands
    {
        private static List<Command> commandsList;

        public static IReadOnlyList<Command> Commands { get => commandsList.AsReadOnly(); }

        public static List<Command> Get()
        {
            commandsList = new List<Command>();
            commandsList.Add(new HelloCommand());
            commandsList.Add(new CyrToLatCommand());
            //TODO: Add more commands here

            //client = new TelegramBotClient(AppSettings.Token);
            //var hook = string.Format(AppSettings.URL, "api/message/update");
            //await client.SetWebhookAsync(hook);
            return commandsList;
        } 
    }
}
