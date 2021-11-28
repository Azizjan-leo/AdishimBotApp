//using AdishimBotApp.Models;
//using AdishimBotApp.Services;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Telegram.Bot;
//using Telegram.Bot.Args;
//using Telegram.Bot.Types;

//namespace AdishimBotApp.Commands
//{
//    public class GameStopCommand : Command
//    {
//        public override List<string> Names => new List<string>() { @"/gamestop", "Oyunni dawamlash", "Оюнни давамлаш"};

//        public override async Task Execute(Message msg, TelegramBotClient client)
//        {
//            var chatId = msg.Chat.Id;            

//            var res = await GameService.Stop(chatId);

//            if(res.Spec == Special.IsCroro)
//                await client.SendTextMessageAsync(chatId, res.Msg, replyToMessageId: msg.MessageId);
//            else
//                await client.SendTextMessageAsync(chatId, res.Msg, replyToMessageId: msg.MessageId);
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
