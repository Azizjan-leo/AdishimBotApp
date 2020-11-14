using System.Threading.Tasks;

namespace AdishimBotApp.Services
{
    public static class TranslitService
    {
        static string[] uly_up  = { "Gh", "Zh", "Ng", "Ch", "Sh", "Yu", "Ya", "A", "E", "B", "W", "G", "D", "Ё", "J", "Z", "I", "Y", "K", "Q", "L", "M", "N", "O", "Ö", "P", "R", "S", "T", "U", "Ü", "F", "X", "H" };
        static string[] cyr_up  = { "Ғ",  "Ж",  "Ң",  "Ч",  "Ш",  "Ю",  "Я",  "А", "Ә", "Б", "В", "Г", "Д", "Е", "Җ", "З", "И", "Й", "К", "Қ", "Л", "М", "Н", "О", "Ө", "П", "Р", "С", "Т", "У", "Ү", "Ф", "Х", "Һ" };
       
        static string[] uly_low = { "gh", "zh", "ng", "ch", "sh", "yu", "ya", "a", "e", "b", "w", "g",  "d", "ё", "j", "z", "i", "y", "k", "q", "l", "m", "n", "o", "ö", "p", "r", "s", "t", "u", "ü", "f", "x", "h" };
        static string[] cyr_low = { "ғ",  "ж",  "ң",  "ч",  "ш",  "ю",  "я",  "а", "ә", "б", "в", "г",  "д", "е", "җ", "з", "и", "й", "к", "қ", "л", "м", "н", "о", "ө", "п", "р", "с", "т", "у", "ү", "ф", "х", "һ" };

        public static async Task<string> CyrToUly(string str)
        {
           
            for (int i = 0; i <= 33; i++)
            {
                str = str.Replace(cyr_up[i], uly_up[i]);
                str = str.Replace(cyr_low[i], uly_low[i]);
            }
            return str;
        }

        public static async Task<string> UlyToCyr(string str)
        {
            for (int i = 0; i <= 33; i++)
            {
                str = str.Replace(uly_up[i], cyr_up[i]);
                str = str.Replace(uly_low[i], cyr_low[i]);
            }
            return str;
        }
    }
}