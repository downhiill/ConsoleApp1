using ConsoleApp1.Commands;
using ConsoleApp1.Extensions;
using ConsoleApp1.GeometricShapeCalculator.Infrastructure;
using ConsoleApp1_Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConsoleApp1
{
    /// <summary>
    /// Представляет основное приложение для работы с различными фигурами.
    /// </summary>
    public class App
    {
        /// <summary>
        /// Коллекция фигур, используемая в приложении.
        /// </summary>
        public ShapeCollection ShapeCollection { get; } = new ShapeCollection();

        /// <summary>
        /// Список команд, доступных для выполнения в приложении.
        /// </summary>
        private readonly List<ICommand> commands;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="App"/> и настраивает доступные команды.
        /// </summary>
        public App()
        {
            commands = new List<ICommand>
            {
            new CommandCreateCircle(ShapeCollection),
            new CommandCreateRectangle(ShapeCollection),
            new CommandCreateTriangle(ShapeCollection),
            new CommandCreateSquare(ShapeCollection),
            new CommandCreatePolygon(ShapeCollection),
            new CommandDisplayTotalArea(ShapeCollection),
            new CommandDisplayTotalPerimetrs(ShapeCollection),
            new CommandSaveData(ShapeCollection),
            new CommandLoadData(ShapeCollection),
            new CommandExit()
            };
            commands.Add(new CommandHelp(commands));
        }

        /// <summary>
        /// Обрабатывает выбор команды пользователя и выполняет соответствующие действия.
        /// </summary>
        /// <returns>Возвращает <c>true</c>, если обработка команды была успешной, <c>false</c> в противном случае.</returns>
        public void Run()
        {
            while(true)
            {
                var (commandKey, parameters) = CommandParser.GetCommandAndParameters();
                var command = commands.FirstOrDefault(c => c.Name == commandKey);

                if (command != null)
                {
                    CommandExtensions.TryExecute(command, parameters);
                }
                else
                {
                    Console.WriteLine("Неизвестная команда. Введите 'помощь' для списка команд.");
                }
            }
            
        }
    }

}

