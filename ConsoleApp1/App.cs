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
    public class App: ShapeCollection
    {
        private readonly Dictionary<string, IApp> commands = new Dictionary<string, IApp>();
        public App()
        {
            // Регистрация команд
            commands["добавить_круг"] = new CreateCircleCommand();
            commands["добавить_прямоугольник"] = new CreateRectangleCommand();
            commands["добавить_треугольник"] = new CreateTriangleCommand();
            commands["добавить_квадрат"] = new CreateSquareCommand();
            commands["добавить_многоугольник"] = new CreatePolygonCommand();
            commands["показать_сумму_площадей"] = new DisplayTotalAreaCommand();
            commands["показать_периметры"] = new DisplayTotalPerimetrsCommand();
            commands["выход"] = new ExitCommand();
        }

        /// <summary>
        /// Обрабатывает выбор фигуры и выполняет соответствующие действия.
        /// </summary>
        private void HandleShapeSelection()
        {
            Console.Clear();
            string text = "Список команд";
            PrintCenteredText(text);
            PrintLine();
            // Печать доступных команд
            foreach (var command in commands.Keys)
            {       
                Console.Write("\t"+ command +"\n");
                
            }
            PrintLine();
            string input = Console.ReadLine()?.Trim();
            if (input != null)
            {
                var parts = input.Split(new[] { ' ' }, 2);
                var commandKey = parts[0].ToLower();
                var parameters = parts.Length > 1 ? parts[1] : "";

                if (commands.TryGetValue(commandKey, out IApp command))
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
        }

        private static void PrintCenteredText(string text)
        {
            int width = Console.WindowWidth;
            int textLength = text.Length;
            int spaces = (width - textLength) / 2;

            Console.WriteLine(new string(' ', spaces) + text);
        }
        private void PrintLine()
        {
            int width = Console.WindowWidth;
            string line = new string('_', width);
            Console.WriteLine(line);
        }
        /// <summary>
        /// Запускает основной цикл приложения для обработки выбора пользователя.
        /// </summary>
        public void Run()
        {
            while (true)
            {
                HandleShapeSelection();
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }
    }
}
