
namespace AdishimBotApp.Models
{
    public static class Alfabet
    {
        public static Letter[] Letters =
        {
            new Letter(1, "Ғ", "ғ", "GH", "gh", "ﻍ", "ﻏ", "ﻐ", "ﻎ", true, true, true),
            new Letter(2, "Ж", "ж", "ZH", "zh", "ژ", "ژ", "ﮋ", "ﮋ", false, false, true), 
            new Letter(3, "Ң", "ң", "NG", "ng", "ﯓ", "ﯕ", "ﯖ", "ﯔ", true, true, true), 
            new Letter(4, "Ч", "ч", "CH", "ch", "ﭺ", "ﭼ", "ﭽ", "ﭻ", true, true, true), 
            new Letter(5, "Ш", "ш", "SH", "sh", "ﺵ", "ﺷ", "ﺸ", "ﺶ", true, true, true), 
            new Letter(6, "Ю", "ю", "YU", "yu", "ﻳﯘ", "ﻳﯘ", "ﻳﯘ", "ﻳﯘ", false, false, false), 
            new Letter(7, "Я", "я", "YA", "ya", "ﻳﺎ", "ﻳﺎ", "ﻳﺎ", "ﻳﺎ", false, false, false), 
            new Letter(8, "А", "а", "A", "a", "ﺋﺎ", "ﺋﺎ", "ﺎ", "ﺎ", false, false, true), 
            new Letter(9, "Ә", "ә", "E", "e", "ﺋﻪ", "ﺋﻪ", "ﻪ", "ﻪ", false, false, true), 
            new Letter(10, "Б", "б", "B", "b", "ﺏ", "ﺑ", "ﺒ", "ﺐ", true, true, true), 
            new Letter(11, "В", "в", "W", "w", "ﯞ", "ﯞ", "ﯟ", "ﯟ", true, false, true), 
            new Letter(12, "Г", "г", "G", "g", "گ", "ﮔ", "ﮕ", "ﮓ", true, true, true), 
            new Letter(13, "Д", "д", "D", "d", "ﺩ", "ﺩ", "ﺪ", "ﺪ", false, false, true), 
            new Letter(14, "Е", "е", "Ё", "ё", "ﺋﯥ", "ﺋﯧ", "ﯧ", "ﯥ", true, true, true), 
            new Letter(15, "Җ", "җ", "J", "j", "ﺝ", "ﺟ", "ﺠ", "ﺞ", true, true, true), 
            new Letter(16, "З", "з", "Z", "z", "ﺯ", "ﺯ", "ﺰ", "ﺰ", false, true, true), 
            new Letter(17, "И", "и", "I", "i", "ﺋﻰ", "ﺋ", "ى", "ﻰ", true, true, true), 
            new Letter(18, "Й", "й", "Y", "y", "ﻱ", "ﻳ", "ﻴ", "ﻲ", true, true, true), 
            new Letter(19, "К", "к", "K", "k", "ﻙ", "ﻛ", "ﻜ", "ﻚ", true, true, true), 
            new Letter(20, "Қ", "қ", "Q", "q", "ﻕ", "ﻗ", "ﻘ", "ﻖ", true, true, true), 
            new Letter(21, "Л", "л", "L", "l", "ﻝ", "ﻟ", "ﻠ", "ﻞ", true, true, true), 
            new Letter(22, "М", "м", "M", "m", "ﻡ", "ﻣ", "ﻤ", "ﻢ", true, true, true), 
            new Letter(23, "Н", "н", "N", "n", "ﻥ", "ﻧ", "ﻨ", "ﻦ", true, true, true), 
            new Letter(24, "О", "о", "O", "o", "ﺋﻮ", "ﺋﻮ", "ﻮ", "ﻮ", false, false, true), 
            new Letter(25, "Ө", "ө", "Ö", "ö", "ﺋﯚ", "ﺋﯚ", "ﯚ", "ﯚ", false, false, true), 
            new Letter(26, "П", "п", "P", "p", "ﭖ", "ﭘ", "ﭙ", "ﭗ", true, true, true), 
            new Letter(27, "Р", "р", "R", "r", "ﺭ", "ﺭ", "ﺮ", "ﺮ", false, false, true), 
            new Letter(28, "С", "с", "S", "s", "ﺱ", "ﺳ", "ﺴ", "ﺲ", true, true, true), 
            new Letter(29, "Т", "т", "T", "t", "ﺕ", "ﺗ", "ﺘ", "ﺖ", true, true, true), 
            new Letter(30, "У", "у", "U", "u", "ﺋﯘ", "ﺋﯘ", "ﯘ", "ﯘ", false, true, true), 
            new Letter(31, "Ү", "ү", "Ü", "ü", "ﺋﯜ", "ﺋﯜ", "ﯜ", "ﯜ", false, false, true), 
            new Letter(32, "Ф", "ф", "F", "f", "ﻑ", "ﻓ", "ﻔ", "ﻒ", true, true, true), 
            new Letter(33, "Х", "х", "X", "x", "ﺥ", "ﺧ", "ﺨ", "ﺦ", true, true, true),
            new Letter(34, "Һ", "һ", "H", "h", "ﻫ", "ﻫ", "ﻬ", "ﮭ", true, true, true),
            new Letter(35, ",", ",", ",", ",", "،", "،", "،", "،", false, false, true),
            new Letter(36, "?", "?", "?", "?", "؟", "؟", "؟", "؟", false, false, true)
        };       
    }
}
