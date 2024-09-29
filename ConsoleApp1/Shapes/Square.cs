using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [Serializable]
    /// <summary>
    /// Представляет квадрат с заданной шириной и высотой.
    /// </summary>
    internal class Square : Shape
    {
        /// <summary>
        /// Ширина и высота квадрата.
        /// </summary>
        public double A { get; set; }

        /// <summary>
        /// Инициализация нового экземпляра квадрата с указаной шириной и высотой.
        /// </summary>
        /// <param name="a">Ширина и высота квадрата</param>
        public Square() { }
        public Square(double a)
        {
            A = a;
        }

        /// <summary>
        /// Переопределяет метод для вычисления площади квадрата.
        /// </summary>
        /// <returns>Площадь квадрата.</returns>
        /// <remarks>
        /// Площадь квадрата вычисляется как квадрат длины его стороны.
        /// </remarks>
        public override double S() => A * A;

        /// <summary>
        /// Переопределяет метод для вычисления периметра квадрата.
        /// </summary>
        /// <returns>Периметр квадрата.</returns>
        /// <remarks>
        /// Периметр квадрата вычисляется как четырёхкратная длина его стороны.
        /// </remarks>
        public override double P() => 4 * A;

        /// <summary>
        /// Форматирует данные о квадрате в строку для вывода.
        /// </summary>
        /// <returns>Строка, представляющая данные о квадрате.</returns>
        /// <remarks>
        /// Строка содержит информацию о стороне, периметре и площади квадрата.
        /// </remarks>
        public override string GetFormattedData() =>
        $"Фигура: Square, Сторона: {A}, Периметр: {P()}, Площадь: {S()}";

        /// <summary>
        /// Возвращает команду для создания квадрата.
        /// </summary>
        /// <returns>Строка команды для создания квадрата.</returns>
        public override string GetCommand() =>
            $"добавить_квадрат [{A}]";
    }
}
