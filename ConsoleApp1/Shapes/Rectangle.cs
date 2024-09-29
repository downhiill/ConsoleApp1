using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [Serializable]
    /// <summary>
    /// Представляет прямоугольник с заданной шириной и высотой.
    /// </summary>
    internal class Rectangle : Shape
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
        public override double S() => Width * Height;

        /// <summary>
        /// Переопределение метода для вычисления периметра.
        /// </summary>
        /// <returns>Периметр прямоугольника</returns>
        public override double P() => 2 * (Width + Height);

        /// <summary>
        /// Форматирует данные о прямоугольнике в строку для вывода.
        /// </summary>
        /// <returns>Строка, представляющая прямоугольник с его шириной, высотой, периметром и площадью.</returns>
        public override string GetFormattedData() =>
        $"Фигура: Rectangle, Ширина: {Width}, Высота: {Height}, Периметр: {P()}, Площадь: {S()}";

        /// <summary>
        /// Возвращает команду для создания прямоугольника.
        /// </summary>
        /// <returns>Строка команды для создания прямоугольника.</returns>
        public override string GetCommand() =>
            $"добавить_прямоугольник [{Width};{Height}]";
    }
}

