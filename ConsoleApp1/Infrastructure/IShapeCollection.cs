using System;
using System.Collections.Generic;

namespace ConsoleApp1.Infrastructure
{
    /// <summary>
    /// Интерфейс для коллекции фигур, поддерживающий вычисление площади и периметра.
    /// </summary>
    public interface IShapeCollection : IEnumerable<Shape>
    {
        /// <summary>
        /// Вычисляет общую площадь фигур указанного типа.
        /// </summary>
        /// <typeparam name="T">Тип фигуры, для которой необходимо вычислить площадь.</typeparam>
        /// <returns>Общая площадь фигур указанного типа.</returns>
        double S<T>() where T : Shape;

        /// <summary>
        /// Вычисляет общую площадь всех фигур в коллекции.
        /// </summary>
        /// <returns>Общая площадь всех фигур.</returns>
        double S();

        /// <summary>
        /// Вычисляет периметр фигур указанного типа.
        /// </summary>
        /// <typeparam name="T">Тип фигуры, для которой необходимо вычислить периметр.</typeparam>
        /// <returns>Периметр фигур указанного типа в виде строки.</returns>
        string P<T>() where T : Shape;

        /// <summary>
        /// Вычисляет периметр всех фигур в коллекции.
        /// </summary>
        /// <returns>Периметр всех фигур в виде строки.</returns>
        string P();

        /// <summary>
        /// Добавляет фигуру в коллекцию.
        /// </summary>
        /// <param name="shape">Фигура, которую необходимо добавить в коллекцию.</param>
        void Add(Shape shape);
    }
}