using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Extensions
{
    internal static class CommandExtensions
    {
        public static void TryExecute(this ICommand command, string parameters)
        {
            if (command != null)
            {
                try
                {
                    command.Execute(parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Неизвестная команда. Пожалуйста, попробуйте снова.");
            }
        }
    }
}
