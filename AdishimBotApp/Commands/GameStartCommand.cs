﻿using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace AdishimBotApp.Commands
{
    public class GameStartCommand : Command
    {
        public override List<string> Names => new () { @"/gamestart", "Oynayli", "Ойнайли" };

        public override async Task Execute(Message msg, ITelegramBotClient client)
        {
            var chatId = msg.Chat.Id;            

            var res2 = await GameService.Start(chatId);
            await client.SendTextMessageAsync(chatId, res2.Msg);
        }

        private async Task CheckAnswer(Message msg, ITelegramBotClient client)
        {
            var chatId = msg.Chat.Id;
            var messageId = msg.MessageId;
            var msgText = msg.Text;
            var author = msg.From;

            var res = await GameService.CheckAnswer(chatId, msgText, author);

            if (res != null)
            {
                if(res.Spec == Special.IsCroro)
                {
                    var inlineKeyboard = new InlineKeyboardMarkup(new[]
                        {
                            new []
                            {
                                InlineKeyboardButton.WithCallbackData("Yëngi oyun", "NewGame")
                            }
                        }
                    );

                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: res.Msg,
                        replyMarkup: inlineKeyboard
                    );
                    return;
                }
                _ = await client.SendTextMessageAsync(chatId, res.Msg, replyToMessageId: messageId);

                if(res.IsSuccess)
                    await Execute(msg, client);
            }

            return;
        }

        public override async Task<bool> TryExecute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var msg = update.Message;

            await CheckAnswer(msg, botClient);
            

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
