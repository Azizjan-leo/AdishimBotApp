using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace AdishimBotApp.Commands
{
    public class StartCrocodileCommand : Command
    {
        public override List<string> Names => new () { @"/startcrocodile", "Timsah oynayli", "Timsaq oynayli", "Тимсаһ ойнайли", "Тимсақ ойнайли" };

        public override async Task Execute(Message msg, ITelegramBotClient client)
        {
            var chatId = msg.Chat.Id;

            InlineKeyboardMarkup inlineKeyboard = null;

            var res = await GameService.CrocoStart(chatId, msg.From);

            if (res.IsSuccess)
            {
                inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Söz", "NewWord")
                    }
                });
            }

            await client.SendTextMessageAsync(
                   chatId: chatId,
                   text: res.Msg,
                   replyMarkup: inlineKeyboard,
                   replyToMessageId: msg.MessageId,
                   parseMode: ParseMode.MarkdownV2
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
