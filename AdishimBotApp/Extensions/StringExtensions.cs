using AdishimBotApp.Models;
using System;
using System.Linq;

namespace AdishimBotApp.Extantions
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input.First().ToString().ToUpper() + input.Substring(1)
            };



        public static string NormalizeArab(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            string output = string.Empty;


            for (int i = 0; i < input.Length; i++)
            {
                output += ((int)input[i]) switch
                {
                    64378 => (char)1670,
                    64472 => (char)1735,
                    65169 => (char)1576,
                    65170 => (char)1576, // b
                    65198 => (char)1585,
                    65200 => (char)1586,
                    65203 => (char)1587, // s 
                    65204 => (char)1587, // s
                    65206 => (char)1588, // sh
                    _ => input[i],
                };
                //if (input[i] == 64472)
                //    output += (char)1735;
                //if ((i + 1 < input.Length && input[i + 1] == 1609) || (i > 0 && input[i - 1] == 1609))
                //{
                //    output += ((int)input[i]) switch
                //    {
                //        64378 => (char)1670,
                //        64472 => (char)1735,
                //        65169 => (char)1576,
                //        65170 => (char)1576, // b
                //        65198 => (char)1585,
                //        65200 => (char)1586,
                //        65203 => (char)1587, // s 
                //        65204 => (char)1587, // s
                //        65206 => (char)1588, // sh
                //        _ => input[i],
                //    };
                //}
                //else
                //    output += input[i];
            }

            return output;
        }

    }
}
