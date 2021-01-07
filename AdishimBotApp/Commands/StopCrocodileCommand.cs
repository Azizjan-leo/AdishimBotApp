using AdishimBotApp.Models;
using AdishimBotApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace AdishimBotApp.Commands
{
    public class StopCrocodileCommand : Command
    {
        public override List<string> Names => new List<string>() { @"/stopcrocodile", "Timsah tamam", "Timsaq tamam", "Тимсаһ тамам", "Тимсақ тамам" };

        public override async Task Execute(Message msg, TelegramBotClient client)
        {
            var res = await GameService.CrocoStop(msg.Chat.Id);

            await client.SendTextMessageAsync(
                   chatId: msg.Chat.Id,
                   text: res.Msg,
                   replyToMessageId: msg.MessageId
               );
        }

     

        public override async Task<bool> TryExecute(MessageEventArgs e, TelegramBotClient client)
        {
            var msg = e.Message;
            
            foreach (var name in Names)
            {
                if (msg.Text.Contains(name) || msg.Text.Contains(Names[0] + Bot.BotName))
                {
                    await Execute(msg, client);
                    return true;
                }
            }
            
            return false;
        }
    }

}
