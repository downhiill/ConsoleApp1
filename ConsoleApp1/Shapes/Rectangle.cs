using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    [Serializable]
    /// <summary>
    /// Представляет прямоугольник с заданной шириной и высотой.
    /// </summary>
    internal class Rectangle : Shape
    {
        /// <summary>
        /// Ширина прямоугольника.
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Высота прямоугольника.
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр прямоугольника с указанной шириной и высотой.
        /// </summary>
        /// <param name="width">Ширина прямоугольника.</param>
        /// <param name="height">Высота прямоугольника.</param>
        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Rectangle"/>.
        /// </summary>
        public Rectangle() { }

        /// <summary>
        /// Переопределяет метод для вычисления площади.
        /// </summary>
        /// <returns>Площадь прямоугольника.</returns>
        public override double S() => Width * Height;

        /// <summary>
        /// Переопределяет метод для вычисления периметра.
        /// </summary>
        /// <returns>Периметр прямоугольника.</returns>
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

        /// <summary>
        /// Сериализует данные прямоугольника в массив байтов для сохранения в бинарный файл.
        /// </summary>
        /// <returns>Массив байтов, представляющий прямоугольник в бинарном формате.</returns>
        public override byte[] SaveToBinary()
        {
            var data = new List<byte>();
            data.Add(4); // Уникальный идентификатор для прямоугольника
            data.AddRange(BitConverter.GetBytes(Width));
            data.AddRange(BitConverter.GetBytes(Height));
            return data.ToArray();
        }

        /// <summary>
        /// Загружает данные прямоугольника из бинарного потока.
        /// </summary>
        /// <param name="stream">Поток, из которого будут загружены данные.</param>
        public override void LoadFromBinary(FileStream stream)
        {
            var dataBytes = new byte[sizeof(double) * 2];
            stream.Read(dataBytes, 0, dataBytes.Length);
            Width = BitConverter.ToDouble(dataBytes, 0);
            Height = BitConverter.ToDouble(dataBytes, sizeof(double));
        }
    }
}
