using System.Collections.Generic;

namespace AdishimBotApp.Commands
{
    public class GroupJoinCommand : Command
    {
        public override List<string> Names => new() { };

        public override Task Execute(Message message, ITelegramBotClient botClient)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> TryExecute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var text = "Xush kepsiz!";
            await botClient.SendTextMessageAsync(update.Message.Chat.Id, text, replyToMessageId: update.Message.MessageId);
            return true;
        }
    }
}
