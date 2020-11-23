using AdishimBotApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AdishimBotApp.Commands
{
    public class UlyToArabCommand : Command
    {
        public override List<string> Names => new List<string>() { @"/ulytoarab", "ulytoarab", "erepche", "ﻻﺗﺌﻦ ﺋﻪﺭﻩﭘﭽﻪ", "ﻟﺎﺗىﻦ ﺋﻪﺭﻩﭘﭽﻪ", "ﻻﺗﺌﻨﻪﺭﻩﭘﭽﻪ" };

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var text = message.Text;

            text = RemoveCommand(text);

            text = TransliterationService.UlyToArab(text);

            await client.SendTextMessageAsync(chatId, text, replyToMessageId: messageId);
        }
    }
}
