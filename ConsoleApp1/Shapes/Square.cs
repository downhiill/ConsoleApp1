using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// Представляет квадрат с заданной шириной и высотой.
    /// </summary>
    internal class Square : Shape
    {
        /// <summary>
        /// Ширина и высота квадрата.
        /// </summary>
        public double A {  get; set; }

        /// <summary>
        /// Инициализация нового экземпляра квадрата с указаной шириной и высотой.
        /// </summary>
        /// <param name="a">Ширина и высота квадрата</param>
        public Square (double a)
        {
            A = a;
        }

        /// <summary>
        /// Переопределение метода для вычисления площади.
        /// </summary>
        /// <returns>Площадь квадрата</returns>
        public override double GetArea() => A * A;

        /// <summary>
        /// Переопределение метода для вычисления периметра.
        /// </summary>
        /// <returns> Периметр квадрата</returns>
        public override double GetPerimeter() => 4 * A;
    }
}
