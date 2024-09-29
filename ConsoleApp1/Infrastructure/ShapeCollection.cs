using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    /// <summary>
    /// Коллекция геометрических фигур, предоставляющая методы для работы с ними.
    /// </summary>
    public class ShapeCollection : List<Shape>
    {
        /// <summary>
        /// Вычисляет общую площадь всех фигур указанного типа в коллекции.
        /// </summary>
        /// <typeparam name="T">Тип фигур для расчета площади. Тип должен наследовать от <see cref="Shape"/>.</typeparam>
        /// <returns>Общая площадь фигур указанного типа.</returns>
        public double S<T>() where T : Shape
        {
            return this.OfType<T>().Sum(shape => shape.S());
        }

        /// <summary>
        /// Вычисляет общую площадь всех фигур в коллекции.
        /// </summary>
        /// <returns>Общая площадь всех фигур в коллекции.</returns>
        public double S()
        {
            return this.Sum(shape => shape.S());
        }

        /// <summary>
        /// Выводит периметры всех фигур указанного типа в коллекции.
        /// </summary>
        /// <typeparam name="T">Тип фигур для расчета периметра. Тип должен наследовать от <see cref="Shape"/>.</typeparam>
        /// <returns>Периметры фигур указанного типа в виде строки, разделенной переводами строк.</returns>
        public string P<T>() where T : Shape
        {
            return string.Join(Environment.NewLine, this.OfType<T>().Select(shape => shape.P()));
        }

        /// <summary>
        /// Выводит периметры всех фигур в коллекции.
        /// </summary>
        /// <returns>Периметры всех фигур в коллекции в виде строки, разделенной переводами строк.</returns>
        public string P()
        {
            return string.Join(Environment.NewLine, this.Select(shape => shape.P()));
        }
    }
}
