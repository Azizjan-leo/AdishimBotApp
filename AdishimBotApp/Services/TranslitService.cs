using System.Threading.Tasks;

namespace AdishimBotApp.Services
{
    public static class TranslitService
    {
        public static async Task<string> CyrToLat(string str)
        {
            string[] lat_up  = { "A", "E", "B", "W", "G", "Gh", "D", "Ё", "Zh", "J", "Z", "I", "Y", "K", "Q", "L", "M", "N", "Ng", "O", "Ö", "P", "R", "S", "T", "U", "Ü", "F", "X", "H", "Ch", "Sh", "Yu", "Ya" };
            string[] rus_up  = { "А", "Ә", "Б", "В", "Г", "Ғ", "Д", "Е",  "Ж",  "Җ", "З", "И", "Й", "К", "Қ", "Л", "М", "Н",  "Ң", "О", "Ө", "П", "Р", "С", "Т", "У", "Ү", "Ф", "Х", "Һ", "Ч",  "Ш",  "Ю",  "Я" };

            string[] lat_low = { "a", "e", "b", "w", "g", "gh", "d", "ё", "zh", "j", "z", "i", "y", "k", "q", "l", "m", "n", "ng", "o", "ö", "p", "r", "s", "t", "u", "ü", "f", "x", "h", "ch", "sh", "yu", "ya" };
            string[] rus_low = { "а", "ә", "б", "в", "г", "ғ", "д", "е", "ж", "җ", "з", "и", "й", "к", "қ", "л", "м", "н", "ң", "о", "ө", "п", "р", "с", "т", "у", "ү", "ф", "х", "һ", "ч", "ш", "ю", "я" };

            for (int i = 0; i <= 33; i++)
            {
                str = str.Replace(rus_up[i], lat_up[i]);
                str = str.Replace(rus_low[i], lat_low[i]);
            }
            return str;
        }
    }
}