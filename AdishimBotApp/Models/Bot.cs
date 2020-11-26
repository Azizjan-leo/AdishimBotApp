using AdishimBotApp.Commands;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Args;

namespace AdishimBotApp.Models
{
    public static class Bot
    {
        /// <summary>  
        /// Declare Telegrambot object  
        /// </summary>  
        private static readonly TelegramBotClient client = new TelegramBotClient("");
        public static readonly string BotName = "@AdishimBot";

        private static readonly List<Command> commands = new List<Command>()
        {
            new HelloCommand(),
            new CyrToUlyCommand(),
            new UlyToCyrCommand(),
            new UlyToArabCommand(),
            new CyrToArabCommand(),
            new ArabToUlyCommand(),
            new ArabToCyrCommand(),
            new AddWordCommand(),
            new TranslateUyCommand(),
            new TranslateRuCommand(),
            new AddWordsCommand(),
            new GameStartCommand(),
            new GetRatingCommand(),
            new GameStopCommand()
        };

        public static void Start()
        {
            client.OnMessage += OnMessageReceive;
            client.StartReceiving();
        }

        /// <summary>  
        /// Handle bot webhook  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        private static void OnMessageReceive(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == MessageType.Text)
                    PrepareQuestionnaires(e);
        }

        public static async void PrepareQuestionnaires(MessageEventArgs e)
        {
            foreach (var command in commands)
            {
                if (await command.TryExecute(e, client))
                    break;
            }
        }
    }
}
