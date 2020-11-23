﻿using AdishimBotApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AdishimBotApp.Commands
{
    public class ArabToUlyCommand : Command
    {
        public override List<string> Names => new List<string>() { @"/arabtouly", "arabtouly", @"/arabtouly@adishimbot", "arabtouly@adishimbot", "ﻻﺗﺌﻨﭽﻪ", "ereplatinche", "erep latinche" };

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var text = message.Text;

            text = RemoveCommand(text);

            text = TransliterationService.FromArab(text, toUly: true);
            await client.SendTextMessageAsync(chatId, text, replyToMessageId: messageId);
        }
    }
}