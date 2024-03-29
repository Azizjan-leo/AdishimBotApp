﻿using System.Collections.Generic;

namespace AdishimBotApp.Commands
{
    public abstract class Command
    {
        public abstract List<string> Names { get; }

        public abstract Task Execute(Message message, ITelegramBotClient botClient);

        public string RemoveCommand(string text)
        {
            foreach (var name in Names)
            {
                text = text.Replace(name, "", true, null);
            }

            if (text != string.Empty)
            {
                text = text.Replace(Bot.BotName, "");
                if(text != string.Empty)
                    return (text[0] == ' ') ? text.Remove(0, 1) : text;
            }
                
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

        public abstract Task<bool> TryExecute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
    }
}
