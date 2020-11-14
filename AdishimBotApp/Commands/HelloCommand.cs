using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AdishimBotApp.Commands
{
    public class HelloCommand : Command
    {
        public override List<string> Names => new List<string>() { @"/salam", "salam", "Essalamu eleyküm!", "Әссаламу әләйкүм!" };

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
                        

            await client.SendTextMessageAsync(chatId, "Essalamu eleyküm!", replyToMessageId: messageId);

        }
    }
}
