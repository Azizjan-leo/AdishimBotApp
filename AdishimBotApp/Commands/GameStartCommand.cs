using AdishimBotApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AdishimBotApp.Commands
{
    public class GameStartCommand : Command
    {
        public override List<string> Names => new List<string>() { @"/gamestart@AdishimBot", "Oynayli", "Ойнайли", "Oyun bashlandi!" };

        public override async Task Execute(Message msg, TelegramBotClient client)
        {
            var chatId = msg.Chat.Id;
            var messageId = msg.MessageId;
            

            var res2 = await GameService.Start(chatId);
            await client.SendTextMessageAsync(chatId, res2.Msg, replyToMessageId: messageId);
        }

        private async Task CheckAnswer(Message msg, TelegramBotClient client)
        {
            var chatId = msg.Chat.Id;
            var messageId = msg.MessageId;
            var msgText = msg.Text;
            var author = msg.From.Username;

            var res = await GameService.CheckAnswer(chatId, msgText, author);

            await client.SendTextMessageAsync(chatId, res.Msg, replyToMessageId: messageId);
            return;
        }

        public override async Task<bool> TryExecute(MessageEventArgs e, TelegramBotClient client)
        {
            var msg = e.Message;

            if ((msg.ReplyToMessage != null && msg.ReplyToMessage.Type == MessageType.Text && msg.ReplyToMessage.From.IsBot == true)
              && (msg.ReplyToMessage.Text.Contains("Oyun bashlandi!") || msg.ReplyToMessage.Text.Contains("Oyun boliwatidu")))
            {
                await CheckAnswer(msg, client);
                return true;
            }

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
