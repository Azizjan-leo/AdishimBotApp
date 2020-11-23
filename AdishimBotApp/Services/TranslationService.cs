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
        //private readonly ApplicationDbContext _context;
        //public TranslationService(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        //void lol()
        //{
        //    ApplicationDbContext d = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
        //}
        /// <summary>
        /// Adds the given word to database.
        /// </summary>
        /// <param name="word">Word must have value!</param>
        /// <returns>
        /// -1 for null obj or number of the saved objects
        /// </returns>
        public async Task<int> AddWords(List<Word> words)
        {
            ApplicationDbContext _context = new ApplicationDbContext(); 

            if (words == null || words.Count() == 0)
                return -1;

            foreach (var word in words)
            {
                try
                {
                    _context.Words.Add(word);
                     await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    string msg = e.Message;
                }
            }
            return await _context.SaveChangesAsync();
        }

        private async Task<Word> TryTranslate(string text, bool fromRu)
        {
            ApplicationDbContext _context = new ApplicationDbContext();

            return await(fromRu ? _context.Words.FirstOrDefaultAsync(x => x.RuText == text)
                : _context.Words.FirstOrDefaultAsync(x => x.UrText == text));
        }

        public async Task<Word> Translate(string text, bool fromRu)
        {
            if (text == null || text.Length == 0)
                return null;
    

            Word result;

            result = await TryTranslate(text, fromRu);

            if(result == null)
                text = TransliterationService.CyrToArab(text);

            result = await TryTranslate(text, fromRu);

            if (result == null)
                text = TransliterationService.CyrToUly(text);

            result = await TryTranslate(text, fromRu);

            if (result == null)
                text = TransliterationService.FromArab(text, toUly: true);

            result = await TryTranslate(text, fromRu);

            if (result == null)
                text = TransliterationService.FromArab(text, toUly: false);

            result = await TryTranslate(text, fromRu);

            if (result == null)
                text = TransliterationService.UlyToArab(text);

            result = await TryTranslate(text, fromRu);

            if (result == null)
                text = TransliterationService.UlyToCyr(text);

            result = await TryTranslate(text, fromRu);

            return result;
        }
    }
}
