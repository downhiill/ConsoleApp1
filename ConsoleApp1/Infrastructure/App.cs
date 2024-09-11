using ConsoleApp1.Commands;
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
                Console.Clear();
                ConsoleExtensions.PrintCenteredText("Введите команду (или 'помощь' для списка команд):");
                ConsoleExtensions.PrintLine("");
                string input = Console.ReadLine()?.Trim();
                if (input != null)
                {
                    var parts = input.Split(new[] { ' ' }, 2);
                    var commandKey = parts[0].ToLower();
                    var parameters = parts.Length > 1 ? parts[1] : "";

                    var command = commands.FirstOrDefault(c => c.Name == commandKey);

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
                else
                {
                    Console.WriteLine("Неверный ввод.");
                }
                Console.ReadKey();
                
            }
            
        }

    }
}
