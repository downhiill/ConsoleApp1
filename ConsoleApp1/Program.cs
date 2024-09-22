using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// Основной класс приложения, содержащий точку входа.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Основная точка входа в приложение.
        /// </summary>
        /// <param name="args">Массив строковых аргументов командной строки.</param>
        static void Main(string[] args)
        {
            var controller = new App();
            controller.Run();
        }
    }
}
