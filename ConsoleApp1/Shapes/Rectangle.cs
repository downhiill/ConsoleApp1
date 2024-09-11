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
    }
}

