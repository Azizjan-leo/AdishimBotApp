using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AdishimBotApp.Commands
{
    public class HelloCommand : Command
    {
        public override string Name => @"/salam";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            // TODO Command logic -_-

            await client.SendTextMessageAsync(chatId, "Essalamu eleyküm!", replyToMessageId: messageId);

            try
            {
            
            }
            catch (System.Exception e)
            {
                
                //await client.SendTextMessageAsync(353071148, e.Message);

            }

        }
    }
}
