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
   
    }
}
