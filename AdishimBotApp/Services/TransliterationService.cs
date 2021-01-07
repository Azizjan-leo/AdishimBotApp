using System;
using AdishimBotApp.Models;

namespace AdishimBotApp.Services
{
    public static class TransliterationService
    {
            
        public static string FromArab(string text, bool toUly)
        {
            var result = string.Empty;

            bool start = true;

            text = toUly ? text.Replace('ﺍ', 'a').Replace('ﻭ', 'o').Replace('ﻩ', 'e') : text.Replace('ﺍ', 'а').Replace('ﻭ', 'о').Replace('ﻩ', 'ә');
       
            for (int i = 0; i < text.Length; i++)
            {

                switch (text[i])
                {
                    case (char)1567:
                        result += "?";
                        start = true;
                        continue;
                    case '!':
                        result += "!";
                        start = true;
                        continue;
                    case '.':
                        result += '.';
                        start = true;
                        continue;
                    default:
                        break;
                }

                string str;

                if (i < text.Length - 1)
                {
                    str = Alfabet.GetLetter(new string(new char[] { text[i], text[i + 1] }), fromArab: true)?.GetInCase(start, Uly: toUly);
                    if (str != null)
                        i++;
                    else
                        str = Alfabet.GetLetter(new string(new char[] { text[i] }), fromArab: true)?.GetInCase(start, Uly: toUly);
                }
                else
                    str = Alfabet.GetLetter(new string(new char[] { text[i] }), fromArab: true)?.GetInCase(start, Uly: toUly);

                if (str != null)
                    start = false;
                else
                    str = text[i].ToString();

                result += str;
            }

            return result;
        }

        public static void RemoveAt<T>(ref T[] arr, int index)
        {
            for (int a = index; a < arr.Length - 1; a++)
            {
                // moving elements downwards, to fill the gap at [index]
                arr[a] = arr[a + 1];
            }
            // finally, let's decrement Array's size by one
            Array.Resize(ref arr, arr.Length - 1);
        }

        public static string CyrToArab(string text)
        {
            string result = string.Empty;
            bool connNext = false;

            text = text.ToLower();

            for (int i = 0; i < text.Length; i++)
            {
                var letter = Alfabet.GetLetter(text[i].ToString(), fromCyr: true);

                if (letter == null)
                {
                    result += text[i];
                    continue;
                }


                if (i == 0 || Alfabet.GetLetter(text[i - 1].ToString(), fromCyr: true) == null) // start
                {
                    string tmp = letter.ArabStart;

                    if (i == text.Length - 1 || Alfabet.GetLetter(text[i + 1].ToString(), fromCyr: true) == null)
                        tmp = letter.Arab;

                    result += tmp;

                    connNext = letter.ConnNext;
                }
                else if (i == text.Length - 1 || Alfabet.GetLetter(text[i + 1].ToString(), fromCyr: true) == null) // end
                {
                    if (connNext)
                    {
                        result += letter.ArabEnd;
                    }
                    else
                    {
                        var tmp = Alfabet.GetSpec(letter);
                        if (tmp != null)
                        {
                            result += tmp;
                            connNext = false;
                            continue;
                        }
                        result += letter.Arab;
                    }
                    connNext = false;
                }
                else // center
                {
                    if (connNext)
                    {
                        result += letter.ArabCenter;
                    }
                    else
                    {
                        var tmp = Alfabet.GetSpec(letter);
                        if (tmp != null)
                        {
                            result += tmp;
                            connNext = false;
                            continue;
                        }

                        if (text[i + 1] == '?' || text[i + 1] == '!' || text[i + 1] == ',' || Alfabet.GetLetter(text[i + 1].ToString(), fromCyr: true) == null)
                        {
                            result += connNext ? letter.ArabEnd : letter.Arab;
                            continue;
                        }
                        
                        result += letter.ArabStart;
                    }
                    connNext = letter.ConnNext;
                }
            }

            return result;//.NormalizeArab();
        }

        public static string UlyToArab(string text)
        {
            string result = string.Empty;
            bool connNext = false;

            text = text.ToLower();

            for (int i = 0; i < text.Length; i++)
            {
                Letter letter = null;

                if(i + 1 < text.Length)
                    letter = Alfabet.GetLetter(text[i].ToString() + text[i + 1].ToString(), fromCyr: false);
                if(letter == null)
                    letter = Alfabet.GetLetter(text[i].ToString(), fromCyr: false);

                if (letter == null)
                {
                    result += text[i];
                    continue;
                }

                i += letter.UlyDown.Length - 1; 

                if (i == 0 || Alfabet.GetLetter(text[i - 1].ToString(), fromCyr: false) == null) // start
                {
                    string tmp = letter.ArabStart;

                    if (i == text.Length - 1 || Alfabet.GetLetter(text[i + 1].ToString(), fromCyr: false) == null)
                        tmp = letter.Arab;

                    result += tmp;

                    connNext = letter.ConnNext;
                }
                else if (i == text.Length - 1 || Alfabet.GetLetter(text[i + 1].ToString(), fromCyr: false) == null) // end
                {
                    if (connNext)
                    {
                        result += letter.ArabEnd;
                    }
                    else
                    {
                        var tmp = Alfabet.GetSpec(letter);
                        if (tmp != null)
                        {
                            result += tmp;
                            connNext = false;
                            continue;
                        }
                        result += letter.Arab;
                    }
                    connNext = false;
                }
                else // center
                {
                    if (connNext)
                    {
                        result += letter.ArabCenter;
                    }
                    else
                    {
                        var tmp = Alfabet.GetSpec(letter);
                        if (tmp != null)
                        {
                            result += tmp;
                            connNext = false;
                            continue;
                        }

                        if (text[i + 1] == '?' || text[i + 1] == '!' || text[i + 1] == ',' || Alfabet.GetLetter(text[1 + 1].ToString(), fromCyr: false) == null)
                        {
                            result += connNext ? letter.ArabEnd : letter.Arab;
                            continue;
                        }

                        result += letter.ArabStart;
                    }
                    connNext = letter.ConnNext;
                }
            }
            return result;
        }

        public static string UlyToCyr(string str)
        {
            foreach (var letter in Alfabet.Letters)
            {
                str = str.Replace(letter.UlyUp, letter.CyrUp);
                str = str.Replace(letter.UlyDown, letter.CyrDown);
            }
            return str;
        }

        public static string CyrToUly(string str)
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