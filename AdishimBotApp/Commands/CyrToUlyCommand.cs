﻿using AdishimBotApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AdishimBotApp.Commands
{
    public class CyrToUlyCommand : Command
    {
        public override List<string> Names => new List<string>() { @"/cyrtouly", "cyrtouly", @"/cyrtouly@adishimbot", "cyrtouly@adishimbot" };

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var text = message.Text;

            foreach (var name in Names)
            {
                text = text.Replace(name, "", true, null);
            }

            text = await TranslitService.CyrToUly(text);
            await client.SendTextMessageAsync(chatId, text, replyToMessageId: messageId);
        }
    }
}