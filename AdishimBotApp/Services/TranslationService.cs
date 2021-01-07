using AdishimBotApp.Extantions;
using AdishimBotApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdishimBotApp.Services
{
    public class TranslationService
    {
    
        /// <summary>
        /// Adds the given word to database.
        /// </summary>
        /// <param name="word">Word must have value!</param>
        /// <returns>
        /// -1 for null obj or number of the saved objects
        /// </returns>
        public async Task<TaskResult> AddWords(List<Word> words)
        {
            ApplicationDbContext _context = new ApplicationDbContext();


            if (words == null || words.Count() == 0)
                return new TaskResult (false,"No words were forwarded");

            var result = new TaskResult(true, "");

            foreach (var word in words)
            {
                try
                {
                    word.Capitalize();
                    _context.Words.Add(word);
                     await _context.SaveChangesAsync();
                    result.Msg += $"{word.UrText} - {word.RuText}\n\n";
                }
                catch (Exception e)
                {                    
                }
            }

            return result;
        }

        private async Task<List<Word>> TryTranslate(string text, bool fromRu)
        {
            var _context = new ApplicationDbContext();

            return await(fromRu ? _context.Words.Where(x => x.RuText == text).ToListAsync()
                : _context.Words.Where(x => x.UrText == text).ToListAsync());
        }

        public async Task<List<Word>> Translate(string text, bool fromRu)
        {
            if (text == null || text.Length == 0)
                return null;
    

            List<Word> resultList = new List<Word>();
            
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
