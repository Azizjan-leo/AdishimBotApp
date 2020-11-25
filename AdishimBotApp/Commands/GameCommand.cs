using AdishimBotApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AdishimBotApp.Commands
{
    public class GameCommand : Command
    {
        public override List<string> Names => new List<string>() { @"/gamestart", "/gamestart@AdishimBot", "Oynayli", "Ойнайли", "Oyun bashlandi!" };

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var msgText = message.Text;
            var author = message.From.Username;
            
            if(!msgText.Contains(Names[0]) && !msgText.Contains(Names[1]))
            {
                var res = await GameService.CheckAnswer(chatId, msgText, author);

                if (res.IsSuccess == true)
                    await client.SendTextMessageAsync(chatId, res.Msg, replyToMessageId: messageId);
                return;
            }

            var res2 = await GameService.Start(chatId);
                await client.SendTextMessageAsync(chatId, res2.Msg, replyToMessageId: messageId);

        }
    }
}
