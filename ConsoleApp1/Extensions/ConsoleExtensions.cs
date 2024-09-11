using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1_Extensions
{
    /// <summary>
    /// Предоставляет методы расширения для работы с консолью.
    /// </summary>
    static class ConsoleExtensions
    {
        /// <summary>
        /// Выводит заданный текст, выровненный по центру окна консоли.
        /// </summary>
        /// <param name="text">Текст, который нужно вывести.</param>
        public static void PrintCenteredText(this string text)
        {
            int width = Console.WindowWidth;
            int textLength = text.Length;
            int spaces = (width - textLength) / 2;

            Console.WriteLine(new string(' ', spaces) + text);
        }
        /// <summary>
        /// Выводит горизонтальную линию, ширина которой соответствует ширине окна консоли.
        /// </summary>
        public static void PrintLine()
        {
            int width = Console.WindowWidth;
            string line = new string('_', width);
            Console.WriteLine(line);
        }
    }
}
