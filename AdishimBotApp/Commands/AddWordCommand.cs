using AdishimBotApp.Models;
using AdishimBotApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AdishimBotApp.Commands
{
    public class AddWordCommand : Command
    {
        public override List<string> Names => new () { "ДобавитьСлово", "СөзҚошуш" };

        public override async Task Execute(Message msg, ITelegramBotClient client)
        {
            var chatId = msg.Chat.Id;
            var msgId = msg.MessageId;
            var text = RemoveCommand(msg.Text).Replace("\n", "");
            

            var words = text.Split(';');
            if(words.Length != 2)
            {
                await client.SendTextMessageAsync(chatId, "Peqet ikki söz!", replyToMessageId: msgId);
                return;
            }

            var newWord = new Word()
            {
                UrText = words[0],
                RuText = words[1],
                AuthorId = 0//msg.From.Id
            };
            var ts = new TranslationService();
            var result = await ts.AddWords(new List<Word>() { newWord });
                        
            var response = result.IsSuccess ? "Rexmet, yadlap aldim! 😊" : result.Msg;
            await client.SendTextMessageAsync(chatId, response, replyToMessageId: msgId);

        }

        public override async Task<bool> TryExecute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var msg = update.Message;
         
            foreach (var name in Names)
            {
                if (msg.Text.Contains(name))
                {
                    await Execute(msg, botClient);
                    return true;
                }
            }

            return false;
        }
    }
}
