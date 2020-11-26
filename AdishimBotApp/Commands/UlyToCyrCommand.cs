using AdishimBotApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AdishimBotApp.Commands
{
    public class UlyToCyrCommand : Command
    {
        public override List<string> Names => new List<string>() { @"/ulytocyr@AdishimBot", "ulytocyr", "kirillche", "латин кириллчә", "латинкириллчә" };

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var text = message.Text;

            text = RemoveCommand(text);

            text = TransliterationService.UlyToCyr(text);

            await client.SendTextMessageAsync(chatId, text, replyToMessageId: messageId);
        }

        public override async Task<bool> TryExecute(MessageEventArgs e, TelegramBotClient client)
        {
            var msg = e.Message;
            foreach (var name in Names)
            {
                if (msg.Text.Contains(name))
                {
                    var woCommand = RemoveCommand(msg.Text);
                    if (!string.IsNullOrEmpty(woCommand))
                    {
                        await Execute(msg, client);
                        return true;
                    }
                    else
                    {
                        if (msg.ReplyToMessage != null && msg.ReplyToMessage.Type == MessageType.Text)
                        {
                            await Execute(msg.ReplyToMessage, client);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

    }
}