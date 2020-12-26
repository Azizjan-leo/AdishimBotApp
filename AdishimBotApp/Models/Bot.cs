using AdishimBotApp.Commands;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using System;
using AdishimBotApp.Services;

namespace AdishimBotApp.Models
{
    public static class Bot
    {
        /// <summary>  
        /// Declare Telegrambot object  
        /// </summary>  
        private static readonly TelegramBotClient client = new TelegramBotClient("1404340694:AAER81CpFJUNy9g0tEv0F17IQw5gPO6v8LE");
        public static readonly string BotName = "@AdishimBot";

        private static readonly List<Command> commands = new List<Command>()
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
            new StopCrocodileCommand()
        };

        public static void Start()
        {
            client.OnMessage += OnMessageReceived;
            client.OnCallbackQuery += BotOnCallbackQueryReceived;
            client.StartReceiving();
        }

        private async static void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var callbackQuery = callbackQueryEventArgs.CallbackQuery;
            try
            {
                InlineKeyboardMarkup inlineKeyboard;
                if (callbackQuery.Data == "NewGame")
                {
                    var res1 = await GameService.CrocoStart(callbackQuery.Message.Chat.Id, callbackQuery.From);

                    if (res1.IsSuccess == true)
                    {
                        inlineKeyboard = new InlineKeyboardMarkup(new[]
                            {
                                new []
                                {
                                    InlineKeyboardButton.WithCallbackData("Söz", "NewWord")
                                }
                            }
                        );
                        await client.SendTextMessageAsync(
                             chatId: callbackQuery.Message.Chat.Id,
                             text: $"{res1.Msg}",
                             replyMarkup: inlineKeyboard,
                             parseMode: ParseMode.MarkdownV2
                        );

                    }
                    else
                    {
                        await client.AnswerCallbackQueryAsync(
                              callbackQueryId: callbackQuery.Id,
                              text: $"Oyun boliwatidu 🙃",
                              showAlert: true
                        );
                    }

                    return;
                }

                var res = await GameService.CrocoNewWord(callbackQuery.Message.Chat.Id, callbackQuery.From.Id);

                if (res.IsSuccess == true)
                {
                    inlineKeyboard = new InlineKeyboardMarkup(new[]
                          {
                                new []
                                {
                                    InlineKeyboardButton.WithCallbackData("Yëngi söz", "NewWord")
                                }
                            }
                      );

                    try
                    {
                        await client.EditMessageTextAsync(callbackQuery.Message.Chat.Id, messageId: callbackQuery.Message.MessageId, $"Oyun bashlandi\\! [Bëshi](tg://user?id={callbackQuery.From.Id})", replyMarkup: inlineKeyboard, parseMode: ParseMode.MarkdownV2);

                    }
                    catch (Exception)
                    {
                    }

                }
                await client.AnswerCallbackQueryAsync(
                      callbackQueryId: callbackQuery.Id,
                      text: $"{res.Msg}",
                      showAlert: true
                  );
            }
            catch (Exception e)
            {
            }

        }

        /// <summary>  
        /// Handle bot webhook  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        private static void OnMessageReceived(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == MessageType.Text)
                PrepareQuestionnaires(e);
        }

        public static async void PrepareQuestionnaires(MessageEventArgs e)
        {
            foreach (var command in commands)
            {
                if (await command.TryExecute(e, client))
                    break;
            }
        }
    }
}
