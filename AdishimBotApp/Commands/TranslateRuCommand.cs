using System.Collections.Generic;

namespace AdishimBotApp.Commands
{
    public class TranslateRuCommand : Command
    {
        public override List<string> Names => new () { @"/touyghur", "uyghurche", "уйғурчә", "по-уйгурски" };

        public override async Task Execute(Message message, ITelegramBotClient client)
        {

            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var text = RemoveCommand(message.Text).Replace("\n", "");
            
            var res = await TranslationService.Translate(text, fromRu: true);
            if(res?.Count > 0)
            {
                string reply = "";
                foreach (var word in res)
                {
                    reply += $"<b>{word.UrText}</b> <code>({word.Id})</code>\n\n";
                }
                await client.SendTextMessageAsync(chatId, reply, replyToMessageId: messageId, parseMode: ParseMode.Html);
            }
            else
                await client.SendTextMessageAsync(chatId, $"Hëch nersini tapalmidim 🤷‍♂️", replyToMessageId: messageId);
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
