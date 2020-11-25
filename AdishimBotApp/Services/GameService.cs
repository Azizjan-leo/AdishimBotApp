using AdishimBotApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdishimBotApp.Services
{
    public static class GameService
    {
        public static async Task<TaskResult> CheckAnswer(long chatId, string answer, string answerer)
        {
            var context = new ApplicationDbContext();

            var lastGame = await context.Games.OrderByDescending(x => x.ChatId == chatId && x.Closed == false).FirstOrDefaultAsync();

            if(lastGame == null)
            {
                return new TaskResult(false, "Bashlighan oyuni yoq");
            }

            if(lastGame.RightAnswer != answer)
            {
                return new TaskResult(false, "Toghra emes :(");
            }

            lastGame.Closed = true;
            lastGame.RightAnswer = answerer;
            lastGame.EndUtc = DateTime.UtcNow;
            context.Entry(lastGame).State = EntityState.Modified;
            await context.SaveChangesAsync();
            
            return new TaskResult(true, "Toghra jawap! ✨");
        }
        public static async Task<TaskResult> Start(long chatId)
        {
            var context = new ApplicationDbContext();
            
            var lastGame = await context.Games.Where(x=> x.Id == chatId).OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            if(lastGame == null || lastGame.Closed)
            {
                var wordsMax = await context.Words.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                if (wordsMax == null)
                    return new TaskResult(false, "Oyungha sözler yoq");
                
                Random rd = new Random();
                Word word = null;

                for (int i = 0; i < 1000; i++)
                {
                    int rand_num = rd.Next(1, wordsMax.Id);
                    word = await context.Words.Where(x => x.Id == rand_num).FirstOrDefaultAsync();
                    if (word != null)
                        break;
                }

                if (word == null)
                    return new TaskResult(false, "Söz tallap almidim");

                GameType gameType = GameType.RuToUy;
                
                if(lastGame != null)
                {
                    if (lastGame.Type == GameType.RuToUy)
                        gameType = GameType.UyToRu;
                }

                string question = null;
                string answer = null;
                if(gameType == GameType.UyToRu)
                {
                    question = $"{word.UrText} rusche qandaq deydu?";
                    answer = word.RuText;
                }
                else
                {
                    question = $"{word.RuText} uyghurche qandaq deydu?";
                    answer = word.UrText;
                }

                var newGame = new Game()
                {
                    ChatId = chatId,
                    StartUtc = DateTime.UtcNow,
                    Question = question,
                    Type = gameType,
                    RightAnswer = answer,
                    Closed = false
                };

                context.Games.Add(newGame);
                await context.SaveChangesAsync();

                return new TaskResult(true, $"{question}\n\nOyun bashlandi! 😃");
            }

            return new TaskResult(false, "Oyun boliwatidu! 😅");
        }
    }
}
