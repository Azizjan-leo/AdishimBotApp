﻿//using AdishimBotApp.Models;
//using AdishimBotApp.Services;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Telegram.Bot;
//using Telegram.Bot.Args;
//using Telegram.Bot.Types;
//using Telegram.Bot.Types.Enums;

//namespace AdishimBotApp.Commands
//{
//    public class TranslateRuCommand : Command
//    {
//        public override List<string> Names => new List<string>() { @"/touyghur", "uyghurche", "уйғурчә", "по-уйгурски" };

//        public override async Task Execute(Message message, TelegramBotClient client)
//        {

//            var chatId = message.Chat.Id;
//            var messageId = message.MessageId;
//            var text = RemoveCommand(message.Text).Replace("\n", "");
//            var ts = new TranslationService();

//            var res = await ts.Translate(text, fromRu: true);
//            if(res?.Count() > 0)
//            {
//                string reply = "";
//                foreach (var word in res)
//                {
//                    reply += $"<b>{word.UrText}</b> <code>({word.Id})</code>\n\n";
//                }
//                await client.SendTextMessageAsync(chatId, reply, replyToMessageId: messageId, parseMode: ParseMode.Html);
//            }
//            else
//                await client.SendTextMessageAsync(chatId, $"Hëch nersini tapalmidim 🤷‍♂️", replyToMessageId: messageId);
//        }

//        public override async Task<bool> TryExecute(MessageEventArgs e, TelegramBotClient client)
//        {
//            var msg = e.Message;
//            foreach (var name in Names)
//            {
//                if (msg.Text.Contains(name) || msg.Text.Contains(Names[0] + Bot.BotName))
//                {
//                    var woCommand = RemoveCommand(msg.Text);
//                    if (!string.IsNullOrEmpty(woCommand))
//                    {
//                        await Execute(msg, client);
//                        return true;
//                    }
//                    else
//                    {
//                        if (msg.ReplyToMessage != null && msg.ReplyToMessage.Type == MessageType.Text)
//                        {
//                            await Execute(msg.ReplyToMessage, client);
//                            return true;
//                        }
//                    }
//                }
//            }

//            return false;
//        }

//    }
//}
