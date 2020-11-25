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
        public async Task<TaskResult> AddWords(List<Word> words)
        {
            ApplicationDbContext _context = new ApplicationDbContext();

            //var list = await _context.Words.ToListAsync();
            //foreach (var word in list)
            //{
            //    if (word.RuText[0] == ' ')
            //        word.RuText = word.RuText.Remove(0, 1);
            //    if (word.UrText[0] == ' ')
            //        word.UrText = word.UrText.Remove(0, 1);
            //    word.Capitalize();
            //    _context.Entry(word).State = EntityState.Modified;
            //}
            //await _context.SaveChangesAsync();


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
                }
                catch (Exception e)
                {
                    string msg = e.Message;
                    result.IsSuccess = false;
                    result.Msg += msg + "\n\n";
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

            //  var text1 = TransliterationService.CyrToArab(text);
            //  tmp = await TryTranslate(text1, fromRu);
            //  resultList.AddRange(tmp);

            //  var text2 = TransliterationService.CyrToUly(text);
            //  tmp = await TryTranslate(text2, fromRu);
            //  resultList.AddRange(tmp);


            //  var text3 = TransliterationService.FromArab(text, toUly: true);
            //  tmp = await TryTranslate(text3, fromRu);
            //  resultList.AddRange(tmp);


            //  var text4 = TransliterationService.FromArab(text, toUly: false);
            //  tmp = await TryTranslate(text4, fromRu);
            //  resultList.AddRange(tmp);


            //  var text5 = TransliterationService.UlyToArab(text);
            //  tmp = await TryTranslate(text5, fromRu);
            //  resultList.AddRange(tmp);


            //      text = TransliterationService.UlyToCyr(text);
            //  tmp = await TryTranslate(text, fromRu);
            //resultList.AddRange(tmp);

            foreach (var item in resultList)
            {
                item.Capitalize();
            }
            return resultList;
        }
    }
}
