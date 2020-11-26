using AdishimBotApp.Models;
using AdishimBotApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AdishimBotApp.Commands
{
    public class AddWordCommand : Command
    {
        public override List<string> Names => new List<string>() { "ДобавитьСлово", "СөзҚошуш" };

        public override async Task Execute(Message msg, TelegramBotClient client)
        {
            var chatId = msg.Chat.Id;
            var msgId = msg.MessageId;
            var text = RemoveCommand(msg.Text).Replace("\n", "");
            

            var words = text.Split(';');
            if(words.Count() != 2)
            {
                await client.SendTextMessageAsync(chatId, "Peqet ikki söz!", replyToMessageId: msgId);
                return;
            }

            var newWord = new Word()
            {
                UrText = words[0],
                RuText = words[1],
                AuthorId = msg.From.Id
            };
            var ts = new TranslationService();
            var result = await ts.AddWords(new List<Word>() { newWord });
                        
            var response = result.IsSuccess ? "Rexmet, yadlap aldim! 😊" : result.Msg;
            await client.SendTextMessageAsync(chatId, response, replyToMessageId: msgId);

        }

        public override async Task<bool> TryExecute(MessageEventArgs e, TelegramBotClient client)
        {
            var msg = e.Message;
            foreach (var name in Names)
            {
                if (msg.Text.Contains(name))
                {
                    await Execute(msg, client);
                    return true;
                }
            }

            return false;
        }
    }
}
