using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] arr1 = { 'a', 'b' };
            char[] arr2 = new char[arr1.Length + 1];
            //Array.Copy(arr1, 0, arr2, 1, arr1.Length-1);
            arr1.CopyTo(arr2, 1);
            arr2[0] = 'c';
            Console.WriteLine(new string(arr2));
            Console.ReadLine();
        }
    }
}
