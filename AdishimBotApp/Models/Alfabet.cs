using System.Linq;

namespace AdishimBotApp.Models
{
    public static class Alfabet
    {
        internal static string GetSpec(Letter letter)
        {
            switch (letter.CyrDown)
            {
                case "а":
                    return "ﺍ";
                case "о":
                    return "ﻭ";
                case "ә":
                    return "ﻩ";
                default:
                    break;
            }
            return null;
        }
  
        internal static Letter GetLetter(string _letter, bool fromArab = false, bool fromCyr = false)
        {
            if (fromArab)
            {
                Letter letter = default;

                switch (_letter)
                {
                    case "ئا":
                        return Letters.Where(x => x.Index == 8).First();
                    case "ئە":
                        return Letters.Where(x => x.Index == 9).First();
                    case "ب":
                        return Letters.Where(x => x.Index == 10).First();
                    case "ئى":
                        return Letters.Where(x => x.Index == 17).First();
                    case "ئۆ":
                        return Letters.Where(x => x.Index == 25).First();
                    case "ئۇ":
                        return Letters.Where(x => x.Index == 30).First();
                    case "ئۈ":
                        return Letters.Where(x => x.Index == 31).First();
                    case "ئو":
                        return Letters.Where(x => x.Index == 24).First();

                    default:
                        break;
                }
              
                foreach (var item in Alfabet.Letters)
                {
                    if (item.Arab == _letter || item.ArabStart == _letter ||
                        item.ArabCenter == _letter || item.ArabEnd == _letter)
                        return item;
                }

                if(_letter.Length == 1)
                {
                    letter = Letters.Where(x => x.CharCode.Contains(_letter[0])).FirstOrDefault();

                    if (letter != null)
                        return letter;

                    string letter3 = new string(new char[] { _letter[0] });
                    foreach (var item in Alfabet.Letters)
                    {
                        if (item.Arab == letter3 || item.ArabStart == letter3 ||
                         item.ArabCenter == letter3 || item.ArabEnd == letter3)
                            return item;
                    }
                }
                if(_letter.Length == 2)
                {
                    letter = Letters.Where(x => x.ArabChar.Length == 2 && x.ArabChar[0] == _letter[0] && x.ArabChar[1] == _letter[1]).FirstOrDefault();

                    if (letter != null)
                        return letter;
                }
                
                return letter;

            }
            if (fromCyr)
            {
                foreach (var letter in Alfabet.Letters)
                {
                    if (letter.CyrDown == _letter)
                        return letter;
                }
            }
            else
            {
                foreach (var letter in Alfabet.Letters)
                {
                    if (letter.UlyDown == _letter)
                        return letter;
                }
            }
            return null;
        }

        public static Letter[] Letters =
        {
            new Letter(1, new int[]{1594 }, "Ғ", "ғ", "GH", "gh", "ﻍ", "ﻏ", "ﻐ", "ﻎ", true, true, true, new int[]{ 1594 }),
            new Letter(2, new int[]{1688 }, "Ж", "ж", "ZH", "zh", "ژ", "ژ", "ﮋ", "ﮋ", false, false, true), 
            new Letter(3, new int[]{1709 }, "Ң", "ң", "NG", "ng", "ﯓ", "ﯕ", "ﯖ", "ﯔ", true, true, true, new int[]{1709 }), 
            new Letter(4, new int[]{1670 }, "Ч", "ч", "CH", "ch", "ﭺ", "ﭼ", "ﭽ", "ﭻ", true, true, true, new int[]{1670 }), 
            new Letter(5, new int[]{1588 }, "Ш", "ш", "SH", "sh", "ﺵ", "ﺷ", "ﺸ", "ﺶ", true, true, true, new int[]{1588 }), 
            new Letter(6, new int[]{1610, 1735 }, "Ю", "ю", "YU", "yu", "ﻳﯘ", "ﻳﯘ", "ﻳﯘ", "ﻳﯘ", false, false, false), 
            new Letter(7, new int[]{1610, 1575 }, "Я", "я", "YA", "ya", "ﻳﺎ", "ﻳﺎ", "ﻳﺎ", "ﻳﺎ", false, false, false), 
            new Letter(8, new int[]{1575 }, "А", "а", "A", "a", "ﺋﺎ", "ﺋﺎ", "ﺎ", "ﺎ", false, false, true, new int[]{1575 }), 
            new Letter(9, new int[]{1749 }, "Ә", "ә", "E", "e", "ﺋﻪ", "ﺋﻪ", "ﻪ", "ﻪ", false, false, true, new int[]{1749 }), 
            new Letter(10, new int[]{1576 }, "Б", "б", "B", "b", "ﺏ", "ﺑ", "ﺒ", "ﺐ", true, true, true, new int[]{1739, 1576 }), 
            new Letter(11, new int[]{1739 }, "В", "в", "W", "w", "ﯞ", "ﯞ", "ﯟ", "ﯟ", false, false, true), 
            new Letter(12, new int[]{1711 }, "Г", "г", "G", "g", "گ", "ﮔ", "ﮕ", "ﮓ", true, true, true), 
            new Letter(13, new int[]{1583 }, "Д", "д", "D", "d", "ﺩ", "ﺩ", "ﺪ", "ﺪ", false, false, true, new int[]{1583 }), 
            new Letter(14, new int[]{1744 }, "Е", "е", "Ë", "ë", "ﺋﯥ", "ﺋﯧ", "ﯧ", "ﯥ", true, true, true, new int[]{1744 }), 
            new Letter(15, new int[]{1580 }, "Җ", "җ", "J", "j", "ﺝ", "ﺟ", "ﺠ", "ﺞ", true, true, true, new int[]{1580 }), 
            new Letter(16, new int[]{1586 }, "З", "з", "Z", "z", "ﺯ", "ﺯ", "ﺰ", "ﺰ", false, true, true, new int[]{1686, 1586 }), 
            new Letter(17, new int[]{1609 }, "И", "и", "I", "i", "ﺋﻰ", "ى", "ى", "ﻰ", true, true, true, new int[]{65164, 1609 }), 
            new Letter(18, new int[]{1610 }, "Й", "й", "Y", "y", "ﻱ", "ﻳ", "ﻴ", "ﻲ", true, true, true, new int[]{1610 }), 
            new Letter(19, new int[]{1603 }, "К", "к", "K", "k", "ﻙ", "ﻛ", "ﻜ", "ﻚ", true, true, true, new int[]{1603 }), 
            new Letter(20, new int[]{1602 }, "Қ", "қ", "Q", "q", "ﻕ", "ﻗ", "ﻘ", "ﻖ", true, true, true, new int[]{1602 }), 
            new Letter(21, new int[]{1604 }, "Л", "л", "L", "l", "ﻝ", "ﻟ", "ﻠ", "ﻞ", true, true, true, new int[]{1604 }), 
            new Letter(22, new int[]{1605 }, "М", "м", "M", "m", "ﻡ", "ﻣ", "ﻤ", "ﻢ", true, true, true, new int[]{65250, 1605 }), 
            new Letter(23, new int[]{1606 }, "Н", "н", "N", "n", "ﻥ", "ﻧ", "ﻨ", "ﻦ", true, true, true, new int[]{1606 }), 
            new Letter(24, new int[]{1608 }, "О", "о", "O", "o", "ﺋﻮ", "ﺋﻮ", "ﻮ", "ﻮ", false, false, true, new int[]{1608 }), 
            new Letter(25, new int[]{1734 }, "Ө", "ө", "Ö", "ö", "ﺋﯚ", "ﺋﯚ", "ﯚ", "ﯚ", false, false, true, new int[]{1734 }), 
            new Letter(26, new int[]{1662 }, "П", "п", "P", "p", "ﭖ", "ﭘ", "ﭙ", "ﭗ", true, true, true, new int[]{1662 }), 
            new Letter(27, new int[]{1585 }, "Р", "р", "R", "r", "ﺭ", "ﺭ", "ﺮ", "ﺮ", false, false, true, new int[]{ 1585 }), 
            new Letter(28, new int[]{1587 }, "С", "с", "S", "s", "ﺱ", "ﺳ", "ﺴ", "ﺲ", true, true, true, new int[]{1587 }), 
            new Letter(29, new int[]{1578 }, "Т", "т", "T", "t", "ﺕ", "ﺗ", "ﺘ", "ﺖ", true, true, true, new int[]{1578 }), 
            new Letter(30, new int[]{1735 }, "У", "у", "U", "u", "ﺋﯘ", "ﺋﯘ", "ﯘ", "ﯘ", false, true, true, new int[]{1735 }), 
            new Letter(31, new int[]{1736 }, "Ү", "ү", "Ü", "ü", "ﺋﯜ", "ﺋﯜ", "ﯜ", "ﯜ", false, false, true, new int[]{1736 }), 
            new Letter(32, new int[]{1601 }, "Ф", "ф", "F", "f", "ﻑ", "ﻓ", "ﻔ", "ﻒ", true, true, true, new int[]{ 65191 }), 
            new Letter(33, new int[]{1582 }, "Х", "х", "X", "x", "ﺥ", "ﺧ", "ﺨ", "ﺦ", true, true, true, new int[]{1582 }),
            new Letter(34, new int[]{1726 }, "Һ", "һ", "H", "h", "ﻫ", "ﻫ", "ﻬ", "ﮭ", true, true, true, new int[]{1726 }),
            new Letter(35, new int[]{1739 }, "В", "в", "V", "v", "ﯞ", "ﯞ", "ﯟ", "ﯟ", false, false, true),
            new Letter(36, new int[]{1587 }, "Ц", "ц", "C", "c", "ﺱ", "ﺳ", "ﺴ", "ﺲ", true, true, true),
            new Letter(37, new int[]{1744 }, "Е", "е", "Ё", "ё", "ﺋﯥ", "ﺋﯧ", "ﯧ", "ﯥ", true, true, true, new int[]{1744 }),
            new Letter(38, new int[]{1548 }, ",", ",", ",", ",", "،", "،", "،", "،", false, false, true, new int[]{1548 }),
            new Letter(39, new int[]{1567 }, "?", "?", "?", "?", "؟", "؟", "؟", "؟", false, false, true, new int[]{1567 }),
            new Letter(40, new int[]{1563 }, ";", ";", ";", ";", "؛", "؛", "؛", "؛", false, false, true),

        };       
    }
}
