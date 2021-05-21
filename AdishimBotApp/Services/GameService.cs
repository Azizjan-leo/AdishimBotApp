using AdishimBotApp.Extantions;
using AdishimBotApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Game = AdishimBotApp.Models.Game;

namespace AdishimBotApp.Services
{
    public static class GameService
    {
        #region Croco

        public static async Task<TaskResult> CrocoStart(long chatId, User user)
        {
            var context = new ApplicationDbContext();

            var openGame = await context.Games.Where(x => x.ChatId == chatId && x.Type == GameType.UrCroco && !x.Closed).FirstOrDefaultAsync();
            if (openGame != null)
            {
                return new TaskResult(false, $"Oyun boliwatidu\\. [Bëshi](tg://user?id={openGame.StarterUserId})");
            }
            else
            {
                var newGame = new Game()
                {
                    ChatId = chatId,
                    StarterUserId = user.Id,
                    StartUtc = DateTime.UtcNow,
                    Type = GameType.UrCroco
                };

                context.Games.Add(newGame);
                await context.SaveChangesAsync();

                return new TaskResult(true, $"Oyun bashlandi\\! [Bëshi](tg://user?id={user.Id})");
            }
        }

        public static async Task<TaskResult> CrocoStop(long chatId)
        {
            var context = new ApplicationDbContext();

            var openGame = await context.Games.Where(x => x.ChatId == chatId && x.Type == GameType.UrCroco && !x.Closed).OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            if (openGame == null)
                return new TaskResult(false, $"Bashlighan oyunlar yoq 😅");

            openGame.Closed = true;
            context.Entry(openGame).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return new TaskResult(true, $"Tamam 😅");
        }

        public static async Task<TaskResult> CrocoNewWord(long chatId, int senderId)
        {
            var context = new ApplicationDbContext();

            var chatGames = await context.Games.Where(x => x.ChatId == chatId && x.Type == GameType.UrCroco).ToListAsync();

            if (chatGames == null || chatGames.Where(x => !x.Closed).Count() == 0)
            {
                var word = await GetRandomWord();

                var newGame = new Game()
                {
                    ChatId = chatId,
                    StarterUserId = senderId,
                    StartUtc = DateTime.UtcNow,
                    Question = word.UrText,
                    Type = GameType.UrCroco
                };

                context.Games.Add(newGame);
                await context.SaveChangesAsync();

                return new TaskResult(true, $"{newGame.Question}");
            }

            var lastGame = chatGames.Where(x => !x.Closed).OrderByDescending(x => x.Id).FirstOrDefault();

            if (lastGame.StarterUserId != senderId)
                return new TaskResult(false, "Oyun bëshi siz emes! 😅");

            var newWord = await GetRandomWord();

            if (newWord == null)
                return new TaskResult(false, "Söz tallap almidim");

            lastGame.Question = newWord.UrText;

            context.Entry(lastGame).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return new TaskResult(true, $"{lastGame.Question}");
        }

        private static async Task<Word> GetRandomWord()
        {
            var context = new ApplicationDbContext();

            var wordsMax = await context.Words.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (wordsMax == null)
                return null;

            Random rd = new();
            Word word = null;

            for (int i = 0; i < 10000; i++)
            {
                int rand_num = rd.Next(1, wordsMax.Id);
                word = await context.Words.Where(x => x.Id == rand_num).FirstOrDefaultAsync();
                if (word != null)
                    break;
            }

            return word;
        }

        #endregion
        public static async Task<int> GetRating(long chatId, User getter)
        {
            var context = new ApplicationDbContext();

            try
            {
                var result = await context.Games.Where(x => x.ChatId == chatId && x.WinnerUserId == getter.Id && x.Closed).ToListAsync();
                int count = result?.Count ?? 0;

                return count;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public static async Task<TaskResult> Stop(long chatId)
        {
            var context = new ApplicationDbContext();

            var lastGame = await context.Games.Where(x => x.ChatId == chatId && x.Closed == false &&
            (x.Type == GameType.RuToUy || x.Type == GameType.UrToRu)).OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            if (lastGame != null)
            {
                lastGame.EndUtc = DateTime.UtcNow;
                lastGame.Closed = true;
                context.Entry(lastGame).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return new TaskResult(true, $"Tamam 😌");
            }

            return new TaskResult(false, $"Bashlangan oyunlar yoq 🙂");
        }

        public static async Task<TaskResult> CheckAnswer(long chatId, string answer, User answerer)
        {
            var context = new ApplicationDbContext();

            var lastGame = await context.Games.Where(x => x.ChatId == chatId && x.Closed == false).OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            if (lastGame == null)
            {
                return null;
            }

            var words = new List<Word>();

            switch (lastGame.Type)
            {
                case GameType.RuToUy:
                    words = await context.Words.Where(x => x.RuText == lastGame.Question).ToListAsync();
                    break;
                case GameType.UrToRu:
                    words = await context.Words.Where(x => x.UrText == lastGame.Question).ToListAsync();
                    break;
                case GameType.UrCroco:
                    words = await context.Words.Where(x => x.UrText == lastGame.Question).ToListAsync();
                    break;
                default:
                    break;
            }


            if (words == null)
            {
                return new TaskResult(false, $"123 Xata boldi. Administratorni chaqiringlar.");
            }

            answer = answer.FirstCharToUpper();

            if (lastGame.Type == GameType.RuToUy || lastGame.Type == GameType.UrCroco)
            {
                foreach (var word in words)
                {
                    if (word.UrText == answer)
                        lastGame.Closed = true;
                }
            }
            else if (lastGame.Type == GameType.UrToRu)
            {
                foreach (var word in words)
                {
                    if (word.RuText == answer)
                        lastGame.Closed = true;
                }
            }

            if (lastGame.Closed == false)
            {
                return null;
            }


            lastGame.WinnerUserId = answerer.Id;
            lastGame.EndUtc = DateTime.UtcNow;
            context.Entry(lastGame).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var rating = await GetRating(chatId, answerer);

            Special special = lastGame.Type == GameType.UrCroco ? Special.IsCroro : Special.NoSpec;

            return new TaskResult(true, $"Toghra jawap! ✨\n\nRëytingingiz: {rating}", special);
        }

        public static async Task<TaskResult> Start(long chatId)
        {
            var context = new ApplicationDbContext();

            var openGame = await context.Games.Where(x => x.ChatId == chatId && !x.Closed &&
            (x.Type == GameType.RuToUy || x.Type == GameType.UrToRu)).FirstOrDefaultAsync();

            if (openGame != null)
            {
                return new TaskResult(false, $"Oyun boliwatidu. Soal: {openGame.Question}");
            }
            var lastGame = await context.Games.Where(x => x.ChatId == chatId &&
            (x.Type == GameType.RuToUy || x.Type == GameType.UrToRu)).OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            if (lastGame == null || lastGame.Closed)
            {
                var word = await GetRandomWord();

                if (word == null)
                    return new TaskResult(false, "Söz tallap almidim");

                GameType gameType = GameType.RuToUy;

                if (lastGame != null)
                {
                    if (lastGame.Type == GameType.RuToUy)
                        gameType = GameType.UrToRu;
                }

                string question = null;
                string answer = null;
                if (gameType == GameType.UrToRu)
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
                    case GameType.UrToRu:
                        return new TaskResult(true, $"{question} rusche qandaq deydu ?\n\nOyun bashlandi! 😃");
                    default:
                        break;
                }
            }

            return new TaskResult(false, "Oyun boliwatidu! 😅");
        }
    }
}
