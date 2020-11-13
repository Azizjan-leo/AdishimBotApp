using AdishimBotApp.Services;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AdishimBotApp.Commands
{
    public class CyrToLatCommand : Command
    {
        public override string Name => @"/cyrtolat";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            try
            {   
                var chatId = message.Chat.Id;
                var messageId = message.MessageId;

                // TODO Command logic -_-
                var text = await TranslitService.CyrToLat(message.Text);
                await client.SendTextMessageAsync(chatId, text, replyToMessageId: messageId);
            }
            catch (System.Exception)
            {

            }

        }
    }
}