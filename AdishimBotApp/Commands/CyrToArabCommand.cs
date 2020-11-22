using AdishimBotApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AdishimBotApp.Commands
{
    public class CyrToArabCommand : Command
    {
        public override List<string> Names => new List<string>() { @"/cyrtoarab", "cyrtoarab", "әрәпчә", "ﻛﺌﺮﺋﻠﻠﻪﺭﻩﭘﭽﻪ", "ﻛﺌﺮﺋﻠﻞ ﺋﻪﺭﻩﭘﭽﻪ" };

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var text = message.Text;

            text = RemoveCommand(text);

            //text = await TranslitService.CyrToArab(text);

            await client.SendTextMessageAsync(chatId, text, replyToMessageId: messageId);
        }
    }
}
