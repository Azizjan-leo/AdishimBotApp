using AdishimBotApp.Commands;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Args;
using System;
using System.Threading.Tasks;

namespace AdishimBotApp.Models
{
    public static class Bot
    {
        /// <summary>  
        /// Declare Telegrambot object  
        /// </summary>  
        private static readonly TelegramBotClient botClient = new TelegramBotClient("");
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
        };

        public static void Start()
        {
            botClient.OnMessage += OnMessageReceive;
            botClient.StartReceiving();
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
                if (await command.TryExecute(e, botClient))
                    break;
            }
        }
    }
}
