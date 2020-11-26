using AdishimBotApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace AdishimBotApp.Commands
{
    public class GameStopCommand : Command
    {
        public override List<string> Names => new List<string>() { @"/gamestop@AdishimBot", "Oyunni dawamlash", "Оюнни давамлаш"};

        public override async Task Execute(Message msg, TelegramBotClient client)
        {
            var chatId = msg.Chat.Id;            

            var res2 = await GameService.Stop(chatId);
            await client.SendTextMessageAsync(chatId, res2.Msg, replyToMessageId: msg.MessageId);
        }


        public override async Task<bool> TryExecute(MessageEventArgs e, TelegramBotClient client)
        {
            var msg = e.Message;

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
