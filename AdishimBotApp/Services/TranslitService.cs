using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdishimBotApp.Models;

namespace AdishimBotApp.Services
{
    public static class TranslitService
    {
        //static readonly string[] uly_up    = { "Gh", "Zh", "Ng", "Ch", "Sh", "Yu", "Ya", "A",  "E",  "B", "W", "G",  "D", "Ё",  "J", "Z", "I",  "Y", "K", "Q", "L", "M", "N", "O",   "Ö",  "P", "R", "S", "T",  "U",   "Ü",  "F",  "X", "H" };
        //static readonly string[] cyr_up    = { "Ғ",  "Ж",  "Ң",  "Ч",  "Ш",  "Ю",  "Я",  "А",  "Ә",  "Б", "В", "Г",  "Д", "Е",  "Җ", "З", "И",  "Й", "К", "Қ", "Л", "М", "Н", "О",   "Ө",  "П", "Р", "С", "Т",  "У",   "Ү",  "Ф",  "Х", "Һ" };

        //static readonly string[] uly_low   = { "gh", "zh", "ng", "ch", "sh", "yu", "ya", "a",  "e",  "b", "w", "g",  "d", "ё",  "j", "z", "i",  "y", "k", "q", "l", "m", "n", "o",   "ö",  "p", "r", "s", "t",  "u",   "ü",  "f",  "x", "h" };
        //static readonly string[] cyr_low   = { "ғ",  "ж",  "ң",  "ч",  "ш",  "ю",  "я",  "а",  "ә",  "б", "в", "г",  "д", "е",  "җ", "з", "и",  "й", "к", "қ", "л", "м", "н", "о",   "ө",  "п", "р", "с", "т",  "у",   "ү",  "ф",  "х", "һ" };

        //static readonly string[] arab      = { "ﻍ",  "ژ",  "ﯓ",  "ﭺ",  "ﺵ", "ﻳﯘ", "ﻳﺎ", "ﺋﺎ", "ﺋﻪ", "ﺏ", "ﯞ", "گ", "ﺩ", "ﺋﯥ", "ﺝ", "ﺯ", "ﺋﻰ", "ﻱ", "ﻙ", "ﻕ", "ﻝ", "ﻡ", "ﻥ", "ﺋﻮ", "ﺋﯚ", "ﭖ", "ﺭ", "ﺱ", "ﺕ", "ﺋﯘ",  "ﺋﯜ", "ﻑ",  "ﺥ", "ﻫ" };
        //static readonly string[] arabStart = { "ﻍ", "ژ",   "ﯓ",  "ﭺ",  "ﺷ", "ﻳﯘ", "ﻳﺎ", "ﺋﺎ", "ﺋﻪ", "ﺑ", "ﯞ", "ﮔ", "ﺩ", "ﺋﯥ", "ﺝ", "ﺯ", "ﺋﻰ", "ﻳ", "ﻛ", "ﻕ", "ﻝ", "ﻡ", "ﻧ", "ﺋﻮ", "ﺋﯚ", "ﭖ", "ﺭ", "ﺳ", "ﺕ", "ﺋﯘ",  "ﺋﯜ", "ﻑ",  "ﺧ", "ﻫ" };
        //static readonly string[] arabCentr = { "ﻍ", "ژ",   "ﯓ",  "ﭺ",  "ﺵ", "ﻳﯘ", "ﻳﺎ", "ﺎ", "ﺋﻪ", "ﺒ", "ﯞ", "گ", "ﺩ", "ﺋﯥ", "ﺝ", "ﺯ", "ﺋﻰ", "ﻱ", "ﻙ", "ﻕ", "ﻝ", "ﻡ", "ﻥ", "ﺋﻮ", "ﺋﯚ", "ﭖ", "ﺭ", "ﺳ", "ﺕ", "ﺋﯘ", "ﺋﯜ", "ﻑ", "ﺥ", "ﻫ" };
        //static readonly string[] arabEnd   = { "ﻍ", "ژ",   "ﯓ",  "ﭺ",  "ﺵ", "ﻳﯘ", "ﻳﺎ", "ﺎ", "ﺋﻪ", "ﺐ", "ﯞ", "گ", "ﺩ", "ﺋﯥ", "ﺝ", "ﺯ", "ﺋﻰ", "ﻱ", "ﻙ", "ﻕ", "ﻝ", "ﻡ", "ﻥ", "ﺋﻮ", "ﺋﯚ", "ﭖ", "ﺭ", "ﺳ", "ﺕ", "ﺋﯘ", "ﺋﯜ", "ﻑ", "ﺥ", "ﻫ" };

        private static string UlyToArabWord(string word)
        {
            for (int i = 0; i <= word.Length - 1; i++)
            {
                int index = GetIndex(word[i].ToString());

                if (index == 0)
                    continue;

                var letter = Alfabet.Letters.Where(x => x.Index == index).First();

                if (i == 0)
                {
                    word = word.Replace(letter.UlyDown, letter.ArabStart);
                }
                else if (i == word.Length - 1)
                {
                    word = word.Replace(letter.UlyDown, letter.ArabEnd);
                }
                else
                {
                    word = word.Replace(letter.UlyDown, letter.ArabCenter);
                }
            }

            return word;
        }

        private static int GetIndex(string _letter)
        {
            foreach (var letter in Alfabet.Letters)
            {
                if (letter.UlyDown == _letter)
                    return letter.Index;
            }
            return 0;
        }

        public static async Task<string> UlyToArab(string text)
        {
            string[] words = text.Split(' ');

            text = text.ToLower();

            foreach (var word in words)
            {
                if (word == string.Empty)
                    continue;

                var arabWord = UlyToArabWord(word);
                text = text.Replace(word, arabWord);
            }

            return text;
        }

        public static async Task<string> UlyToCyr(string str)
        {
            foreach (var letter in Alfabet.Letters)
            {
                str = str.Replace(letter.UlyUp, letter.CyrUp);
                str = str.Replace(letter.UlyDown, letter.CyrDown);
            }
            return str;
        }

        public static async Task<string> CyrToUly(string str)
        {

            foreach (var letter in Alfabet.Letters)
            {
                str = str.Replace(letter.CyrUp, letter.UlyUp);
                str = str.Replace(letter.CyrDown, letter.UlyDown);
            }
            return str;
        }

    }
}