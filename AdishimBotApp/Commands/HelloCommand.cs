﻿using Telegram.Bot;
using Telegram.Bot.Types;

namespace AdishimBotApp.Commands
{
    public class HelloCommand : Command
    {
        public override string Name => "hello";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            // TODO Command logic -_-

            await client.SendTextMessageAsync(chatId, "Hi!", replyToMessageId: messageId);
        }
    }
}
