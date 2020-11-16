using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdishimBotApp.Models;

namespace AdishimBotApp.Services
{
    public static class TranslitService
    {
        private static int GetIndex(char _letter, bool prev = false)
        {
            char[] arr = { _letter };
            var str = new string(arr);
            if (prev)
            {
                foreach (var letter in Alfabet.Letters)
                {
                    if (letter.Arab == str || letter.ArabStart == str || letter.ArabCenter == str || letter.ArabEnd == str)
                        return letter.Index;
                }
            }
            foreach (var letter in Alfabet.Letters)
            {
                if (letter.UlyDown == str)
                    return letter.Index;
            }
            return 0;
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

        public static async Task<string> UlyToArab(string str)
        {
            char[] text = str.ToLower().ToCharArray();
            bool connNext = false;
            for (int i = 0; i < text.Length; i++)
            {
                int index = GetIndex(text[i]);

                if (index == 0)
                {
                    if (i + 1 < text.Length && (text[i] == 'c') && (text[i + 1] == 'h'))
                        index = Alfabet.Letters.Where(x => x.UlyDown == "ch").First().Index;
                    else
                        continue;
                }
                    

                var letter = Alfabet.Letters.Where(x => x.Index == index).First();

                if (i + 1 < text.Length)
                {
                    var st = new string(new char[] { text[i], text[i + 1] });
                    if (st == "sh" || st == "ch" || st == "gh" || st == "ng" || st == "zh")
                    {
                        var lettr = Alfabet.Letters.Where(x => x.UlyDown == st).First();
                        if (i == 0 || (i > 0 && GetIndex(text[i - 1], true) == 0)) // start
                        {
                            if(i == text.Length - 2 || GetIndex(text[i + 2]) == 0)
                            {
                                text[i] = lettr.Arab[0];
                            }
                            else
                            {
                                text[i] = lettr.ArabStart[0];
                            }
                            RemoveAt<char>(ref text, i + 1);
                            connNext = lettr.ConnNext;
                            continue;
                        }
                        else if (i == text.Length - 2 || GetIndex(text[i + 2]) == 0) // end
                        {
                            text[i] = lettr.ArabEnd[0];
                            RemoveAt<char>(ref text, i + 1);
                            connNext = false;
                            continue;
                        }
                        // center
                        if (connNext)
                        {
                            text[i] = lettr.ArabCenter[0];
                            RemoveAt<char>(ref text, i + 1);
                            connNext = lettr.ConnNext;
                        }
                        else
                        {
                            text[i] = lettr.ArabStart[0];
                            RemoveAt<char>(ref text, i + 1);
                            connNext = lettr.ConnNext;
                        }
                        continue;
                    }

                }

                if (i == 0 || (i > 0 && GetIndex(text[i-1], true) == 0)) // start
                {
                    if(letter.ArabStart.Length > 1)
                    {

                        char[] newarr = new char[text.Length + letter.ArabStart.Length];
                        for (int j = 0; j < text.Length + 1; j++)
                        {
                            if (j < i)
                                newarr[j] = text[j];
                            else if (j == i)
                            {
                                int ind = 0;
                                for (int k = j; ind < letter.ArabStart.Length; k++)
                                {
                                    newarr[k] = letter.ArabStart[ind++];
                                }
                                j += ind - 1;
                                continue;
                            }
                            else
                                newarr[j] = text[j - 1];
                        }
                        text = newarr;
                        i += letter.ArabStart.Length - 3;
                    }
                    else
                    {
                        if(i + 1 < text.Length)
                            text[i] = letter.ArabStart[0];
                        else
                            text[i] = letter.Arab[0];
                    }
                    connNext = letter.ConnNext;
                }
                else if (i == text.Length - 1 || GetIndex(text[i+1]) == 0) // end
                {
                    if (connNext) 
                    {
                        text[i] = letter.ArabEnd[0];
                    }
                    else
                    {
                        if (letter.UlyDown == "a")
                        {
                            text[i] = 'ﺍ';
                            connNext = false;
                            continue;
                        }
                        if (letter.UlyDown == "o")
                        {
                            text[i] = 'ﻭ';
                            connNext = false;
                            continue;
                        }
                        if (letter.UlyDown == "e")
                        {
                            text[i] = 'ﻩ';
                            connNext = false;
                            continue;
                        }
                        text[i] = letter.Arab[0];
                    }
                    connNext = false;
                }
                else // center
                {
                    if (connNext)
                    {
                        text[i] = letter.ArabCenter[0];
                    }
                    else
                    {
                        if (letter.Arab == "ﺋﺎ")
                        {
                            text[i] = 'ﺍ';
                            connNext = false;
                            continue;
                        }
                        if (letter.UlyDown == "o")
                        {
                            text[i] = 'ﻭ';
                            connNext = false;
                            continue;
                        }
                        if (letter.UlyDown == "e")
                        {
                            text[i] = 'ﻩ';
                            connNext = false;
                            continue;
                        }
                        if (letter.ArabStart.Length > 1)
                        {
                            char[] newarr = new char[text.Length + letter.ArabStart.Length];
                            for (int j = 0; j < text.Length + 1; j++)
                            {
                                if (j < i)
                                    newarr[j] = text[j];
                                else if (j == i)
                                {
                                    int ind = 0;
                                    for (int k = j; ind < letter.ArabStart.Length; k++)
                                    {
                                        newarr[k] = letter.ArabStart[ind++];
                                    }
                                    j += ind - 1;
                                    continue;
                                }
                                else
                                    newarr[j] = text[j - 1];
                            }
                            text = newarr;
                            i += letter.ArabStart.Length - 3;
                        }
                        else
                        {
                            text[i] = letter.ArabStart[0];
                        }

                    }
                    connNext = letter.ConnNext;
                }
            }
            str = new string(text);
            return str;
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