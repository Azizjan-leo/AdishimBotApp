using System.Collections.Generic;

namespace AdishimBotApp.Commands
{
    public class AddWordsCommand : Command
    {
        public override List<string> Names => new () { @"/addwords@AdishimBot", "Moshu xetige jawapta sözlerni yëzing, merhemet." };

        public override async Task Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var msgId = message.MessageId;
            var msgText = message.Text;

            if (msgText != Names[0])
            {
                var words = msgText.Replace("\n", ";").Split(';');
                if (words.Length < 2)
                {
                    await client.SendTextMessageAsync(chatId, "Birmu söz bermidingiz 😅.", replyToMessageId: msgId);
                    return;
                }

                var wordsList = new List<Word>();
                
                for(int i = 0; i < words.Length; i+=2)
                {
                    if (words[i] == string.Empty || words[i + 1] == string.Empty)
                        continue;

                    if (words[i][0] == ' ')
                        words[i] = words[i].Remove(0, 1);
                    if (words[i+1][0] == ' ')
                        words[i] = words[i+1].Remove(0, 1);
                    
                    var newWord = new Word()
                    {
                        UrText = words[i],
                        RuText = words[i + 1],
                        AuthorId = message.From.Id
                    };

                    wordsList.Add(newWord);
                }
               
                var result = await TranslationService.AddWords(wordsList);
                var response = result.IsSuccess ? "Rexmet, yadlap aldim! 😊" : result.Msg;
                await client.SendTextMessageAsync(chatId, response, replyToMessageId: msgId);
                return;
            }


        }

        public override async Task<bool> TryExecute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var msg = update.Message;

            if (msg.Text.Contains(Names[0]) || msg.Text.Contains(Names[0] + Bot.BotName))
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Moshu xetige jawapta sözlerni yëzing, merhemet.", replyToMessageId: msg.MessageId, cancellationToken: cancellationToken);
                return true;
            }

            if(msg.ReplyToMessage != null && msg.ReplyToMessage.Text == Names[1] && msg.ReplyToMessage.From.IsBot)
            {
                await Execute(msg, botClient);
                return true;
            }

            return false;
        }
    }
}
