using ConsoleApp1_Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Commands
{
    /// <summary>
    /// Команда, которая выводит список доступных команд и их описание.
    /// </summary>
    internal class CommandHelp : ICommand
    {
        private readonly List<ICommand> commands;
        /// <summary>
        /// Получает имя команды.
        /// </summary>
        /// <value>Имя команды, используемое для её идентификации. В данном случае — "помощь".</value>
        public string Name => "помощь";

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CommandHelp"/> с заданным списком команд.
        /// </summary>
        /// <param name="commands">Список команд, которые будут выведены в помощи.</param>
        /// <exception cref="ArgumentNullException">Вызывается, если параметр <paramref name="commands"/> равен <c>null</c>.</exception>
        public CommandHelp(List<ICommand> commands)
        {
            this.commands = commands ?? throw new ArgumentNullException(nameof(commands));
        }

        /// <summary>
        /// Выполняет команду, выводя список команд и их описание.
        /// </summary>
        /// <param name="parameters">Параметры команды. В данном случае игнорируются.</param>
        public void Execute(string parameters)
        {
            PrintHelp();
        }

        /// <summary>
        /// Выводит список команд и их описание в консоль.
        /// </summary>
        private void PrintHelp()
        {
            Console.Clear();
            ConsoleExtensions.PrintCenteredText("Список команд");
            ConsoleExtensions.PrintLine("");

            foreach (var command in commands)
            {
                Console.WriteLine($"\t{command.Name}");
            }

            ConsoleExtensions.PrintLine("");
        }
    }
}
