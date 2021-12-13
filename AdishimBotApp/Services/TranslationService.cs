using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AdishimBotApp.Services
{
    public static class TranslationService
    {

        /// <summary>
        /// Adds the given word to database.
        /// </summary>
        /// <param name="word">Word must have value!</param>
        /// <returns>
        /// -1 for null obj or number of the saved objects
        /// </returns>
        public static async Task<TaskResult> AddWords(List<Word> words)
        {
            var context = new ApplicationDbContext();
            if (words == null || words.Count == 0)
                return new TaskResult(false, "No words were forwarded");

            var result = new TaskResult(true, "");

            foreach (var word in words)
            {
                try
                {
                    word.Capitalize();
                    context.Words.Add(word);
                    await context.SaveChangesAsync();
                    result.Msg += $"{word.UrText} - {word.RuText}\n\n";
                }
                catch (Exception e)
                {
                }
            }

            return result;
        }

        private static async Task<List<Word>> TryTranslate(string text, bool fromRu)
        {
            var context = new ApplicationDbContext();
            return await (fromRu ? context.Words.Where(x => x.RuText == text).ToListAsync()
                : context.Words.Where(x => x.UrText == text).ToListAsync());
        }

        public static async Task<List<Word>> Translate(string text, bool fromRu)
        {
            if (text == null || text.Length == 0)
                return null;


            var resultList = new List<Word>();

            var tmp = await TryTranslate(text, fromRu);
            resultList.AddRange(tmp);

            foreach (var item in resultList)
            {
                item.Capitalize();
            }
            return resultList;
        }
    }
}
