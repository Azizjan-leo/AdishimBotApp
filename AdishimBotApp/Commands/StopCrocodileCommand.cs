using System.Collections.Generic;

namespace AdishimBotApp.Commands
{
    public class StopCrocodileCommand : Command
    {
        public override List<string> Names => new () { @"/stopcrocodile", "Timsah tamam", "Timsaq tamam", "Тимсаһ тамам", "Тимсақ тамам" };

        public override async Task Execute(Message msg, ITelegramBotClient client)
        {
            var res = await GameService.CrocoStop(msg.Chat.Id);

            await client.SendTextMessageAsync(
                   chatId: msg.Chat.Id,
                   text: res.Msg,
                   replyToMessageId: msg.MessageId
               );
        }


        public override async Task<bool> TryExecute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var msg = update.Message;
            
            foreach (var name in Names)
            {
                if (msg.Text.Contains(name) || msg.Text.Contains(Names[0] + Bot.BotName))
                {
                    await Execute(msg, botClient);
                    return true;
                }
            }
            
            return false;
        }
    }

}
