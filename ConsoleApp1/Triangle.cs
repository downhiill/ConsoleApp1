using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// Представляет треугольник с заданными сторонами.
    /// </summary>
    internal class Triangle : Shape
    {
        /// <summary>
        /// Сторона A 
        /// </summary>
        public double A { get; set; }

        /// <summary>
        /// Сторона В 
        /// </summary>
        public double B { get; set; }

        /// <summary>
        /// Сторона С 
        /// </summary>
        public double C { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр треугольника с указанными длинами сторон.
        /// </summary>
        /// <param name="a">Сторона a.</param>
        /// <param name="b">Сторона b.</param>
        /// <param name="c">Сторона c.</param>
        public Triangle(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        /// <summary>
        /// Переопределение метода для вычисления площади треугольника по формуле Герона.
        /// </summary>
        /// <returns>Площадь треугольника.</returns>
        public override double GetArea()
        {
            double s = GetPerimeter() / 2;
            return Math.Sqrt(s * (s - A) * (s - B) * (s - C));
        }

        /// <summary>
        /// Переопределение метода для вычисления периметра треугольника.
        /// </summary>
        /// <returns>Периметр треугольника</returns>
        public override double GetPerimeter() => A + B + C;

        /// <summary>
        /// Переопределение метода для отображения информации о треугольнике.
        /// </summary>
        public override void Display()
        {
            Console.WriteLine($"Треугольник: Сторона A = {A}, Сторона B = {B}, Сторона C = {C}");
            Console.WriteLine($"Площадь = {GetArea():F2}");
            Console.WriteLine($"Периметр = {GetPerimeter():F2}");
        }

        /// <summary>
        /// Переопределение метода для получения строки с данными о треугольнике.
        /// </summary>
        /// <returns></returns>
        public override string GetInputData()
        {
            return $"Треугольник: Сторона A = {A}, Сторона B = {B}, Сторона C = {C}";
        }

        /// <summary>
        /// Создание нового экземпляра треугольника на основе ввода пользователя.
        /// </summary>
        /// <returns>Новый экземпляр треугольника или <c>null</c>, если ввод неверен.</returns>
        public static Triangle CreateTriangle()
        {
            Console.Write("Введите длину стороны A треугольника: ");
            if (double.TryParse(Console.ReadLine(), out double a))
            {
                Console.Write("Введите длину стороны B треугольника: ");
                if (double.TryParse(Console.ReadLine(), out double b))
                {
                    Console.Write("Введите длину стороны C треугольника: ");
                    if (double.TryParse(Console.ReadLine(), out double c))
                    {
                        return new Triangle(a, b, c);
                    }
                    else
                    {
                        Console.WriteLine("Неверное значение стороны C.");
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine("Неверное значение стороны B.");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Неверное значение стороны A.");
                return null;
            }
        }
    }
}
