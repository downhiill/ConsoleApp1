using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// Представляет прямоугольник с заданной шириной и высотой.
    /// </summary>
    internal class Rectangle: Shape
    {
        /// <summary>
        /// Ширина прямоугольника
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// Высота прямоугольника
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Инициализация нового экземпляра прямоугольника с указаной шириной и высотой.
        /// </summary>
        /// <param name="width">Ширина прямоугольника.</param>
        /// <param name="height">Высота прямоугольника</param>
        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Переопределение метода для вычисления площади.
        /// </summary>
        /// <returns>Площадь прямоугольника</returns>
        public override double GetArea() => Width * Height;

        /// <summary>
        /// Переопределение метода для вычисления периметра.
        /// </summary>
        /// <returns>Периметр прямоугольника</returns>
        public override double GetPerimeter() => 2 * (Width + Height);
        /// <summary>
        /// Переопределение метода для отображения информации о прямоугольнике.
        /// </summary>
        public override void Display()
        {
            Console.WriteLine($"Прямоугольник: Ширина = {Width}, Высота = {Height}");
            Console.WriteLine($"Площадь = {GetArea():F2}");
            Console.WriteLine($"Периметр = {GetPerimeter():F2}");
        }
        /// <summary>
        /// Переопределяет метод для получения строки с данными о прямоугольнике.
        /// </summary>
        /// <returns> Данные о прямоугольнике.</returns>
        public override string GetInputData()
        {
            return $"Прямоугольник: Ширина = {Width}, Высота = {Height}";
        }

        /// <summary>
        /// Создает новый экземпляр прямоугольника на основе ввода пользователя.
        /// </summary>
        /// <returns>Новый экземпляр прямоугольника или <c>null</c>, если ввод неверен.</returns>
        public static Rectangle CreateRectangle()
        {
            Console.Write("Введите ширину прямоугольника: ");
            if (double.TryParse(Console.ReadLine(), out double width))
            {
                Console.Write("Введите высоту прямоугольника: ");
                if (double.TryParse(Console.ReadLine(), out double height))
                {
                    return new Rectangle(width, height);
                }
                else
                {
                    Console.WriteLine("Неверное значение высоты.");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Неверное значение ширины.");
                return null;
            }
        }
    }
}

