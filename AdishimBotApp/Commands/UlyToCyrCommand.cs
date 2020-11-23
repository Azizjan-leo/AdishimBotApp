using AdishimBotApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AdishimBotApp.Commands
{
    public class UlyToCyrCommand : Command
    {
        public override List<string> Names => new List<string>() { @"/ulytocyr", @"/ulytocyr@adishimbot", "ulytocyr", "ulytocyr@adishimbot", "kirillche", "латин кириллчә", "латинкириллчә" };

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var text = message.Text;

            text = RemoveCommand(text);

            text = TransliterationService.UlyToCyr(text);

            await client.SendTextMessageAsync(chatId, text, replyToMessageId: messageId);
        }
    }
}