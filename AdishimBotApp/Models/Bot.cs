using AdishimBotApp.Commands;
using System.Collections.Generic;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Exceptions;

namespace AdishimBotApp.Models
{
    public class Bot
    {
        /// <summary>  
        /// Declare Telegrambot object  
        /// </summary>  
        private static readonly TelegramBotClient client = new ("1404340694:AAHhDCig4m70DTyUxm9iBP-4994U8VfOY7A");

        public static readonly string BotName = "@AdishimBot";

        private static readonly List<Command> commands = new ()
        {
            new HelloCommand(),
            new CyrToUlyCommand(),
            new UlyToCyrCommand(),
            new UlyToArabCommand(),
            new CyrToArabCommand(),
            new ArabToUlyCommand(),
            new ArabToCyrCommand(),
            new AddWordCommand(),
            new TranslateUyCommand(),
            new TranslateRuCommand(),
            new AddWordsCommand(),
            new GameStartCommand(),
            new GetRatingCommand(),
            new GameStopCommand(),
            new StartCrocodileCommand(),
            new StopCrocodileCommand(),
        };

        public static void Start(CancellationTokenSource cts)
        {
            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };

            client.StartReceiving(
                OnMessageReceived,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken: cts.Token);

          //  client.OnCallbackQuery += BotOnCallbackQueryReceived;
           
        }

        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        //private async static void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        //{
        //    var callbackQuery = callbackQueryEventArgs.CallbackQuery;
        //    try
        //    {
        //        InlineKeyboardMarkup inlineKeyboard;
        //        if (callbackQuery.Data == "NewGame")
        //        {
        //            var res1 = await GameService.CrocoStart(callbackQuery.Message.Chat.Id, callbackQuery.From);

        //            if (res1.IsSuccess == true)
        //            {
        //                inlineKeyboard = new InlineKeyboardMarkup(new[]
        //                    {
        //                        new []
        //                        {
        //                            InlineKeyboardButton.WithCallbackData("Söz", "NewWord")
        //                        }
        //                    }
        //                );
        //                await client.SendTextMessageAsync(
        //                     chatId: callbackQuery.Message.Chat.Id,
        //                     text: $"{res1.Msg}",
        //                     replyMarkup: inlineKeyboard,
        //                     parseMode: ParseMode.MarkdownV2
        //                );

        //            }
        //            else
        //            {
        //                await client.AnswerCallbackQueryAsync(
        //                      callbackQueryId: callbackQuery.Id,
        //                      text: $"Oyun boliwatidu 🙃",
        //                      showAlert: true
        //                );
        //            }

        //            return;
        //        }

        //        var res = await GameService.CrocoNewWord(callbackQuery.Message.Chat.Id, callbackQuery.From.Id);

        //        if (res.IsSuccess == true)
        //        {
        //            inlineKeyboard = new InlineKeyboardMarkup(new[]
        //                  {
        //                        new []
        //                        {
        //                            InlineKeyboardButton.WithCallbackData("Yëngi söz", "NewWord")
        //                        }
        //                    }
        //              );

        //            try
        //            {
        //                await client.EditMessageTextAsync(callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId, $"Oyun bashlandi\\! [Bëshi](tg://user?id={callbackQuery.From.Id})", replyMarkup: inlineKeyboard, parseMode: ParseMode.MarkdownV2);

        //            }
        //            catch (Exception)
        //            {
        //            }

        //        }
        //        await client.AnswerCallbackQueryAsync(
        //              callbackQueryId: callbackQuery.Id,
        //              text: $"{res.Msg}",
        //              showAlert: true
        //          );
        //    }
        //    catch (Exception e)
        //    {
        //    }

        //}

        /// <summary>  
        /// Handle bot webhook  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        private static async Task OnMessageReceived(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update.Message.Text != null) 
            {
                foreach (var command in commands)
                {
                    if (await command.TryExecute(botClient, update, cancellationToken))
                        break;
                }
            }
        }
    }
}
