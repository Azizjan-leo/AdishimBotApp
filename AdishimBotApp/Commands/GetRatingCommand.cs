﻿using AdishimBotApp.Models;
using AdishimBotApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace AdishimBotApp.Commands
{
    public class GetRatingCommand : Command
    {
        public override List<string> Names => new List<string>() { @"/myrating" };

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            var answer = await GameService.GetRating(chatId, message.From);

            await client.SendTextMessageAsync(chatId, $"Rëytingingiz: {answer}", replyToMessageId: messageId);

        }

        public override async Task<bool> TryExecute(MessageEventArgs e, TelegramBotClient client)
        {
            var msg = e.Message;
            foreach (var name in Names)
            {
                if (msg.Text.Contains(name) || msg.Text.Contains(Names[0] + Bot.BotName))
                {
                    
                        await Execute(msg, client);
                        return true;
                }
            }

            return false;
        }
    }
}
