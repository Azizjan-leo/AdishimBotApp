//using AdishimBotApp.Models;
//using AdishimBotApp.Services;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Telegram.Bot;
//using Telegram.Bot.Args;
//using Telegram.Bot.Types;
//using Telegram.Bot.Types.ReplyMarkups;

//namespace AdishimBotApp.Commands
//{
//    public class StartCrocodileCommand : Command
//    {
//        public override List<string> Names => new List<string>() { @"/startcrocodile", "Timsah oynayli", "Timsaq oynayli", "Тимсаһ ойнайли", "Тимсақ ойнайли" };

//        public override async Task Execute(Message msg, TelegramBotClient client)
//        {
//            var chatId = msg.Chat.Id;

//            InlineKeyboardMarkup inlineKeyboard = null;

//            var res = await GameService.CrocoStart(chatId, msg.From);

//            if (res.IsSuccess)
//            {
//                inlineKeyboard = new InlineKeyboardMarkup(new[]
//                {
//                    new []
//                    {
//                        InlineKeyboardButton.WithCallbackData("Söz", "NewWord")
//                    }
//                });
//            }

//            await client.SendTextMessageAsync(
//                   chatId: chatId,
//                   text: res.Msg,
//                   replyMarkup: inlineKeyboard,
//                   replyToMessageId: msg.MessageId,
//                   parseMode: Telegram.Bot.Types.Enums.ParseMode.MarkdownV2
//               );

//        }

     

//        public override async Task<bool> TryExecute(MessageEventArgs e, TelegramBotClient client)
//        {
//            var msg = e.Message;
            
//            foreach (var name in Names)
//            {
//                if (msg.Text.Contains(name) || msg.Text.Contains(Names[0] + Bot.BotName))
//                {
//                    await Execute(msg, client);
//                    return true;
//                }
//            }
            
//            return false;
//        }
//    }

//}
