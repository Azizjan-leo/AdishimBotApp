using System.Collections.Generic;

namespace AdishimBotApp.Commands
{
    public class GameStopCommand : Command
    {
        public override List<string> Names => new () { @"/gamestop", "Oyunni dawamlash", "Оюнни давамлаш"};

        public override async Task Execute(Message msg, ITelegramBotClient client)
        {
            var chatId = msg.Chat.Id;            

            var res = await GameService.Stop(chatId);

            if(res.Spec == Special.IsCroro)
                await client.SendTextMessageAsync(chatId, res.Msg, replyToMessageId: msg.MessageId);
            else
                await client.SendTextMessageAsync(chatId, res.Msg, replyToMessageId: msg.MessageId);
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
