using System.Collections.Generic;

namespace AdishimBotApp.Commands
{
    public class UlyToCyrCommand : Command
    {

        public override List<string> Names => new () { @"/ulytocyr", "ulytocyr", "kirillche", "латин кириллчә", "латинкириллчә" };

        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var text = message.Text;

            text = RemoveCommand(text);

            text = TransliterationService.UlyToCyr(text);

            await client.SendTextMessageAsync(chatId, text, replyToMessageId: messageId);
        }

        public override async Task<bool> TryExecute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var msg = update.Message;
            foreach (var name in Names)
            {
                if (msg.Text.Contains(name) || msg.Text.Contains(Names[0] + Bot.BotName))
                {
                    var woCommand = RemoveCommand(msg.Text);
                    if (!string.IsNullOrEmpty(woCommand))
                    {
                        await Execute(msg, botClient);
                        return true;
                    }
                    else
                    {
                        if (msg.ReplyToMessage != null && msg.ReplyToMessage.Type == MessageType.Text)
                        {
                            await Execute(msg.ReplyToMessage, botClient);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

    }
}