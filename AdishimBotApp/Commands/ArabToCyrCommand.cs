using AdishimBotApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AdishimBotApp.Commands
{
    public class ArabToCyrCommand : Command
    {
        public override List<string> Names => new List<string>() { @"/arabtocyr", "arabtocyr", @"/arabtocyr@adishimbot", "arabtocyr@adishimbot", "ﻛىﺮﺋﻠﻠﭽﻪ", "әрәпкириллчә", "әрәп кириллчә" };

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var text = message.Text;

            text = RemoveCommand(text);

            text = TranslitService.FromArab(text, toUly: false);
            await client.SendTextMessageAsync(chatId, text, replyToMessageId: messageId);
        }
    }
}