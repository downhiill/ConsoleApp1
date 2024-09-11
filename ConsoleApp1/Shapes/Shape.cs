using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// Абстрактный базовый класс для геометрических фигур.
    /// </summary>
    public abstract class Shape
    {
        /// <summary>
        /// Получает площадь фигуры.
        /// </summary>
        /// <returns>Площадь фигуры.</returns>
        public abstract double GetArea();

        /// <summary>
        /// Получает периметр фигуры.
        /// </summary>
        /// <returns>Периметр фигуры.</returns>
        public abstract double GetPerimeter();

    }
}
