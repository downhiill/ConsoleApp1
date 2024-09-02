using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// Представляет основное приложение для работы с различными фигурами.
    /// </summary>
    internal class App
    {
        /// <summary>
        /// Список фигур, созданных в приложении.
        /// </summary>
        static List<Shape> shapes = new List<Shape>();

        /// <summary>
        /// Обрабатывает выбор фигуры и выполняет соответствующие действия.
        /// </summary>
        private void HandleShapeSelection()
        {
            Console.Clear();
            Console.WriteLine("Выберите фигуру для расчета:");
            Console.WriteLine("1. Круг");
            Console.WriteLine("2. Прямоугольник");
            Console.WriteLine("3. Треугольник");
            Console.WriteLine("4. Квадрат");
            Console.WriteLine("5. Показать сумму площади фигур");
            Console.WriteLine("6. Показать все периметры");
            Console.WriteLine("7. Выход");


            var choice = Console.ReadLine();
            Shape shape = null;

            switch (choice)
            {
                case "1":
                    shape = Circle.CreateCircle();
                    break;
                case "2":
                    shape = Rectangle.CreateRectangle();
                    break;
                case "3":
                    shape = Triangle.CreateTriangle();
                    break;
                case "4":
                    shape = Square.CreateSquare();
                    break;
                case "5":
                    DisplayTotalArea();
                    break;
                case "6":
                    DisplayTotalPerimeters();
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    return;
            }
            if (shape != null)
            {
                shapes.Add(shape);
                shape.Display(); // Вызов метода Display() для отображения информации о фигуре
            }
            else 
            {
                Console.WriteLine("Не удалось создать фигуру.");
            }
        }

        /// <summary>
        ///  Выводит сумму всех площадей фигур в списке.
        /// </summary>
        private void DisplayTotalArea()
        {
            Console.Clear();
            double totalArea = 0;

            foreach (var shape in shapes)
            {
                totalArea += shape.GetArea();
            }
            Console.WriteLine($"Сумма всех S = {totalArea}");

           
        }

        /// <summary>
        /// Выводит периметры всех фигур в списке.
        /// </summary>
        private void DisplayTotalPerimeters()
        {
            Console.Clear();

            Console.WriteLine("\nПериметры всех фигур:");
            foreach (var shape in shapes)
            {
                Console.WriteLine($"Фигура: {shape.GetType().Name}, P = {shape.GetPerimeter()}");
            }
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
