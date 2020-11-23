using AdishimBotApp.Models;
using AdishimBotApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AdishimBotApp.Commands
{
    public class TranslateUyCommand : Command
    {
        public override List<string> Names => new List<string>() { @"/torussian", "по-русски, адишим", "русчә, адишим" };

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var text = RemoveCommand(message.Text).Replace("\n", "");
            var ts = new TranslationService();
            var res = await ts.Translate(text, fromRu: false);
            if(res != null)
                await client.SendTextMessageAsync(chatId, $"{res.RuText}", replyToMessageId: messageId);
            else
                await client.SendTextMessageAsync(chatId, $"Hëch nersini tapalmidim 🤷‍♂️", replyToMessageId: messageId);
        }
    }
}
