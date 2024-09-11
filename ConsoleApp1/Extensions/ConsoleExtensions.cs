using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class ConsoleExtensions
    {
        public static void PrintCenteredText(string text)
        {
            int width = Console.WindowWidth;
            int textLength = text.Length;
            int spaces = (width - textLength) / 2;

            Console.WriteLine(new string(' ', spaces) + text);
        }

        public static void PrintLine()
        {
            int width = Console.WindowWidth;
            string line = new string('_', width);
            Console.WriteLine(line);
        }
    }
}
