using System.Collections.Generic;

namespace AdishimBotApp.Commands
{
    public class GetRatingCommand : Command
    {
        public override List<string> Names => new () { @"/myrating" };

        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            var answer = await GameService.GetRating(chatId, message.From);

            await client.SendTextMessageAsync(chatId, $"Rëytingingiz: {answer}", replyToMessageId: messageId);

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
