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
        MyList<Shape> shapesList = new MyList<Shape>();
        /// <summary>
        /// Список фигур, созданных в приложении.
        /// </summary>

        /// <summary>
        /// Обрабатывает выбор фигуры и выполняет соответствующие действия.
        /// </summary>
        private void HandleShapeSelection()
        {
            Console.Clear();
            Console.WriteLine("Выберите фигуру для расчета:");

            var menuItems = new Dictionary<int, string>
            {
                { 1, "Круг" },
                { 2, "Прямоугольник" },
                { 3, "Треугольник" },
                { 4, "Квадрат" },
                { 5, "Многоугольник" },
                { 6, "Показать сумму площади фигур" },
                { 7, "Показать все периметры" },
                { 8, "Выход" }
            };

            // Печать меню
            foreach (var item in menuItems)
            {
                Console.WriteLine($"{item.Key}. {item.Value}");
            }

            // Обработка выбора пользователя
            if (int.TryParse(Console.ReadLine(), out int choice) && menuItems.ContainsKey(choice))
            {
                Shape shape = null;
                var actions = new Dictionary<int, Action>
                {
                    { 1, () => shape = Circle.CreateCircle() },
                    { 2, () => shape = Rectangle.CreateRectangle() },
                    { 3, () => shape = Triangle.CreateTriangle() },
                    { 4, () => shape = Square.CreateSquare() },
                    { 5, () => shape = Polygon.CreateRegularPolygon() },
                    { 6, () => DisplayTotalArea() },
                    { 7, () => DisplayTotalPerimeters() },
                    { 8, () => Environment.Exit(0) }
                };

                // Выполнение выбранного действия
                actions[choice]();

                // Обработка созданной фигуры
                if (shape != null)
                {
                    shapesList.Add(shape);
                    shape.Display(); // Вызов метода Display() для отображения информации о фигуре
                }
                else if (choice != 6 && choice != 7 && choice != 8)
                {
                    Console.WriteLine("Не удалось создать фигуру.");
                }
            }
            else
            {
                Console.WriteLine("Неверный выбор, попробуйте снова.");
            }
        }
        /// <summary>
        ///  Выводит сумму всех площадей фигур в списке.
        /// </summary>
        private void DisplayTotalArea()
        {
            Console.Clear();

            shapesList.PrintSquare();

           
        }

        /// <summary>
        /// Выводит периметры всех фигур в списке.
        /// </summary>
        private void DisplayTotalPerimeters()
        {
            Console.Clear();

            shapesList.PrintPerimeter();
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
