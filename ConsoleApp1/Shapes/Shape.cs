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
        /// <returns>Площадь фигуры в виде числа с плавающей запятой.</returns>
        /// <remarks>
        /// Каждый наследующий класс должен переопределить этот метод, 
        /// чтобы предоставить корректное значение площади для конкретного типа фигуры.
        /// </remarks>
        public abstract double S();

        /// <summary>
        /// Получает периметр фигуры.
        /// </summary>
        /// <returns>Периметр фигуры в виде числа с плавающей запятой.</returns>
        /// <remarks>
        /// Каждый наследующий класс должен переопределить этот метод, 
        /// чтобы предоставить корректное значение периметра для конкретного типа фигуры.
        /// </remarks>
        public abstract double P();

        /// <summary>
        /// Форматирует данные о фигуре в строку для вывода.
        /// </summary>
        /// <returns>Строка, представляющая данные о фигуре.</returns>
        /// <remarks>
        /// Каждый наследующий класс должен переопределить этот метод, 
        /// чтобы предоставить форматированное представление данных о фигуре.
        /// </remarks>
        public abstract string GetFormattedData();
        public abstract string GetCommand();
    }
}
