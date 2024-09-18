using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Extensions
{
    /// <summary>
    /// Расширения для интерфейса <see cref="ICommand"/>.
    /// </summary>
    internal static class CommandExtensions
    {

        /// <summary>
        /// Попытка выполнить команду с указанными параметрами.
        /// В случае возникновения исключения, ошибка выводится на консоль.
        /// </summary>
        /// <param name="command">Команда, которую нужно выполнить.</param>
        /// <param name="parameters">Параметры для выполнения команды.</param>
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
