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
            new TranslateRuCommand()
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

        public static void PrepareQuestionnaires(MessageEventArgs e)
        {
            try
            {
                string messageTxt = e.Message.Text.ToLower();
                foreach (var command in commands)
                {
                    if (!command.Contains(messageTxt))
                        continue;

                    if(command.RemoveCommand(messageTxt) != string.Empty)
                    {
                        command.Execute(e.Message, botClient);
                        break;
                    }    
                    if(e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Type == MessageType.Text)
                    {
                        var replyMsgTxt = e.Message.ReplyToMessage.Text;
                        if(replyMsgTxt.Trim() != string.Empty)
                        {
                            command.Execute(e.Message.ReplyToMessage, botClient);
                            break;
                        }
                    }
                    break;
                }
            }
            catch (Exception ex)
            {
                Logger.Messages.Add(ex.Message);
            }

        }
    }
}
