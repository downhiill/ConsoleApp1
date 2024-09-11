using ConsoleApp1.GeometricShapeCalculator.Infrastructure;
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
        private readonly List<ICommand> commands = new List<ICommand>
        {
            new CreateCircleCommand(),
            new CreateRectangleCommand(),
            new CreateTriangleCommand(),
            new CreateSquareCommand(),
            new CreatePolygonCommand(),
            new DisplayTotalAreaCommand(),
            new DisplayTotalPerimetrsCommand(),
            new ExitCommand()
        };

        /// <summary>
        /// Обрабатывает выбор команды пользователя и выполняет соответствующие действия.
        /// </summary>
        /// <returns>Возвращает <c>true</c>, если обработка команды была успешной, <c>false</c> в противном случае.</returns>
        private bool HandleShapeSelection()
        {
            Console.Clear();
            ConsoleExtensions.PrintCenteredText("Список команд");
            ConsoleExtensions.PrintLine();
            // Печать доступных команд
            foreach (var command in commands)
            {
                Console.Write($"\t{command.Name}\n");
                
            }
            ConsoleExtensions.PrintLine();
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
                        command.Execute(this, parameters);
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
            return true;
        }

        /// <summary>
        /// Запускает основной цикл приложения для обработки выбора пользователя.
        /// </summary>
        public void Run()
        {
            if (HandleShapeSelection())
            {

                // Рекурсивный вызов для обработки следующей команды
                Run();
            }
        }
    }
}
