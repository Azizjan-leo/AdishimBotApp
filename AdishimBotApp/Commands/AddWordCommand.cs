using AdishimBotApp.Models;
using AdishimBotApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AdishimBotApp.Commands
{
    public class AddWordCommand : Command
    {
        public override List<string> Names => new List<string>() { "ДобавитьСлово", "СөзҚошуш" };

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var msgId = message.MessageId;
            var text = RemoveCommand(message.Text).Replace("\n", "");
            

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
                AuthorId = message.From.Id
            };
            var ts = new TranslationService();
            var result = await ts.AddWords(new List<Word>() { newWord });
                        
            var response = result.IsSuccess ? "Rexmet, yadlap aldim! 😊" : result.Msg;
            await client.SendTextMessageAsync(chatId, response, replyToMessageId: msgId);

        }
    }
}
