using AdishimBotApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace AdishimBotApp.Commands
{
    public class GameStartCommand : Command
    {
        public override List<string> Names => new List<string>() { @"/gamestart@AdishimBot", "Oynayli", "Ойнайли" };

        public override async Task Execute(Message msg, TelegramBotClient client)
        {
            var chatId = msg.Chat.Id;            

            var res2 = await GameService.Start(chatId);
            await client.SendTextMessageAsync(chatId, res2.Msg);
        }

        private async Task CheckAnswer(Message msg, TelegramBotClient client)
        {
            var chatId = msg.Chat.Id;
            var messageId = msg.MessageId;
            var msgText = msg.Text;
            var author = msg.From.Username;

            var res = await GameService.CheckAnswer(chatId, msgText, author);

            if (res != null)
            {
                _ = await client.SendTextMessageAsync(chatId, res.Msg, replyToMessageId: messageId);

                if(res.IsSuccess)
                    await Execute(msg, client);
            }

            return;
        }

        public override async Task<bool> TryExecute(MessageEventArgs e, TelegramBotClient client)
        {
            var msg = e.Message;

            await CheckAnswer(msg, client);
            

            foreach (var name in Names)
            {
                if (msg.Text.Contains(name))
                {
                    await Execute(msg, client);
                    return true;
                }
            }
            
            return false;
        }
    }
}
