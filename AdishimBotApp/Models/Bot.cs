using AdishimBotApp.Commands;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Args;
using System;

namespace AdishimBotApp.Models
{
    public static class Bot
    {
        /// <summary>  
        /// Declare Telegrambot object  
        /// </summary>  
        private static readonly TelegramBotClient botClient = new TelegramBotClient("");

        private static readonly List<Command> commands = new List<Command>() { new HelloCommand(), new CyrToUlyCommand(), new UlyToCyrCommand() };

        public static void Start()
        {
            botClient.OnMessage += Csharpcornerbotmessage;
            botClient.StartReceiving();
        }

        /// <summary>  
        /// Handle bot webhook  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        private static void Csharpcornerbotmessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == MessageType.Text)
                PrepareQuestionnaires(e);
        }

        public static void PrepareQuestionnaires(MessageEventArgs e)
        {
            try
            {
                string messageTxt = e.Message.Text.ToLower();
                foreach (var command in commands)
                {
                    if (command.Contains(messageTxt))
                    {
                        command.Execute(e.Message, botClient);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Messages.Add(ex.Message);
            }

        }
    }
}
