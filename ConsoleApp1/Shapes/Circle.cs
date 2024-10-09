using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    [Serializable]
    /// <summary>
    /// Представляет круг с его радиусом.
    /// </summary>
    public class Circle : Shape
    {
        /// <summary>
        /// Радиус круга.
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Circle"/>.
        /// </summary>
        public Circle() { }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Circle"/> с указанным радиусом.
        /// </summary>
        /// <param name="radius">Радиус круга.</param>
        public Circle(double radius)
        {
            Radius = radius;
        }

        /// <summary>
        /// Переопределяет метод для вычисления площади круга.
        /// </summary>
        /// <returns>Площадь круга.</returns>
        public override double S() => Math.PI * Radius * Radius;

        /// <summary>
        /// Переопределяет метод для вычисления периметра круга.
        /// </summary>
        /// <returns>Периметр круга.</returns>
        public override double P() => 2 * Math.PI * Radius;

        /// <summary>
        /// Форматирует данные о круге в строку.
        /// </summary>
        /// <returns>Строка, представляющая круг с радиусом, периметром и площадью.</returns>
        public override string GetFormattedData() =>
            $"Фигура: Circle, Радиус: {Radius}, Периметр: {P()}, Площадь: {S()}";

        /// <summary>
        /// Возвращает команду для создания круга.
        /// </summary>
        /// <returns>Строка команды для создания круга.</returns>
        public override string GetCommand() =>
            $"добавить_круг [{Radius}]";

        /// <summary>
        /// Сериализует данные круга в массив байтов для сохранения в бинарный файл.
        /// </summary>
        /// <returns>Массив байтов, представляющий круг в бинарном формате.</returns>
        public override byte[] SaveToBinary()
        {
            var data = new List<byte>();
            data.Add(1); // Уникальный идентификатор для круга
            data.AddRange(BitConverter.GetBytes(Radius));
            return data.ToArray();
        }

        /// <summary>
        /// Загружает данные круга из бинарного потока.
        /// </summary>
        /// <param name="stream">Поток, из которого будут загружены данные.</param>
        public override void LoadFromBinary(FileStream stream)
        {
            var radiusBytes = new byte[sizeof(double)];
            stream.Read(radiusBytes, 0, radiusBytes.Length);
            Radius = BitConverter.ToDouble(radiusBytes, 0);
        }
    }
}
