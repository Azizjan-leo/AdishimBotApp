using AdishimBotApp.Extantions;
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
        public static async Task<int> GetRating(long chatId, string getter)
        {
            var context = new ApplicationDbContext();

            try
            {
                var result = await context.Games.Where(x => x.ChatId == chatId && x.WinnerUsername == getter && x.Closed).ToListAsync();
                int count = result?.Count() ?? 0;

                return count;
            }
            catch(Exception e)
            {
                return -1;
            }
        }

        public static async Task<TaskResult> Stop(long chatId)
        {
            var context = new ApplicationDbContext();

            var lastGame = await context.Games.Where(x => x.ChatId == chatId && x.Closed == false).OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            if(lastGame != null)
            {
                lastGame.EndUtc = DateTime.UtcNow;
                lastGame.Closed = true;
                context.Entry(lastGame).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return new TaskResult(true, $"Tamam 😌");
            }

            return new TaskResult(false, $"Bashlangan oyunlar yoq 🙂");
        }

        public static async Task<TaskResult> CheckAnswer(long chatId, string answer, string answerer)
        {
            var context = new ApplicationDbContext();

            var lastGame = await context.Games.Where(x => x.ChatId == chatId && x.Closed == false).OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            if (lastGame == null)
            {
                return null;
            }

            var words = new List<Word>();

            if (lastGame.Type == GameType.RuToUy)
            {
                words = await context.Words.Where(x => x.RuText == lastGame.Question).ToListAsync();
            }
            else if (lastGame.Type == GameType.UyToRu)
            {
                words = await context.Words.Where(x => x.UrText == lastGame.Question).ToListAsync();
            }

            if (words == null)
            {
                return new TaskResult(false, $"123 Xata boldi. Administratorni chaqiringlar.");
            }

            answer = answer.FirstCharToUpper();

            if (lastGame.Type == GameType.RuToUy)
            {
                foreach (var word in words)
                {
                    if (word.UrText == answer)
                        lastGame.Closed = true;
                }
            }
            else if (lastGame.Type == GameType.UyToRu)
            {
                foreach (var word in words)
                {
                    if (word.RuText == answer)
                        lastGame.Closed = true;
                }
            }
        
            if(lastGame.Closed == false)
            {
                return null;
            }

       
            lastGame.WinnerUsername = answerer;
            lastGame.EndUtc = DateTime.UtcNow;
            context.Entry(lastGame).State = EntityState.Modified;
            await context.SaveChangesAsync();
            var rating = await GetRating(chatId, answerer);
            return new TaskResult(true, $"Toghra jawap! ✨\n\nRëytingingiz: {rating}");
        }
        
        public static async Task<TaskResult> Start(long chatId)
        {
            var context = new ApplicationDbContext();

            var openGame = await context.Games.Where(x => x.ChatId == chatId && !x.Closed).FirstOrDefaultAsync();
            if(openGame != null)
            {
                return new TaskResult(false, $"Oyun boliwatidu. Soal: {openGame.Question}");
            }
            var lastGame = await context.Games.Where(x=> x.ChatId == chatId).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            
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
                    question = $"{word.UrText}";
                    answer = word.RuText;
                }
                else
                {
                    question = $"{word.RuText}";
                    answer = word.UrText;
                }

                var newGame = new Game()
                {
                    ChatId = chatId,
                    StartUtc = DateTime.UtcNow,
                    Question = question,
                    Type = gameType,
                    Closed = false
                };

                context.Games.Add(newGame);
                await context.SaveChangesAsync();

                switch (newGame.Type)
                {
                    case GameType.RuToUy:
                        return new TaskResult(true, $"{question} uyghurche qandaq deydu ?\n\nOyun bashlandi! 😃");
                    case GameType.UyToRu:
                        return new TaskResult(true, $"{question} rusche qandaq deydu ?\n\nOyun bashlandi! 😃");
                    default:
                        break;
                }
            }

            return new TaskResult(false, "Oyun boliwatidu! 😅");
        }
    }
}
