using AdishimBotApp.Models;
using AdishimBotApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AdishimBotApp.Commands
{
    public class TranslateRuCommand : Command
    {
        public override List<string> Names => new List<string>() { @"/touyghur", "uyghurche", "уйғурчә", "по-уйгурски" };

        public override async Task Execute(Message message, TelegramBotClient client)
        {

            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var text = RemoveCommand(message.Text).Replace("\n", "");
            var ts = new TranslationService();

            var res = await ts.Translate(text, fromRu: true);
            if(res?.Count() > 0)
            {
                string reply = "";
                for(int i=0; i < res.Count(); i++)
                {
                    reply += $"{i + 1}. {res[i].UrText}\n\n";
                }
                await client.SendTextMessageAsync(chatId, $"{reply}", replyToMessageId: messageId);
            }
            else
                await client.SendTextMessageAsync(chatId, $"Hëch nersini tapalmidim 🤷‍♂️", replyToMessageId: messageId);
        }
    }
}
