using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Commands
{
    internal class CommandsParser
    {
        /// <summary>
        /// Ожидает ввода команды пользователя и возвращает название команды и параметры.
        /// </summary>
        /// <returns>Кортеж, содержащий название команды и параметры.</returns>
        public static (string commandKey, string parameters) GetCommandAndParameters()
        {
            Console.WriteLine("Введите команду и параметры:");
            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                return (string.Empty, string.Empty);
            }

            var parts = input.Split(new[] { ' ' }, 2);
            var commandKey = parts[0].ToLower();
            var parameters = parts.Length > 1 ? parts[1] : "";

            return (commandKey, parameters);
        }
    }
}
