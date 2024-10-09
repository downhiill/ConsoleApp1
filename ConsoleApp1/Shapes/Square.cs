using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    [Serializable]
    /// <summary>
    /// Представляет квадрат с заданной длиной стороны.
    /// </summary>
    internal class Square : Shape
    {
        /// <summary>
        /// Длина стороны квадрата.
        /// </summary>
        public double A { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр квадрата с заданной длиной стороны.
        /// </summary>
        /// <param name="a">Длина стороны квадрата.</param>
        public Square(double a)
        {
            A = a;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Square"/>.
        /// </summary>
        public Square() { }

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

        /// <summary>
        /// Сериализует данные квадрата в массив байтов для сохранения в бинарный файл.
        /// </summary>
        /// <returns>Массив байтов, представляющий квадрат в бинарном формате.</returns>
        public override byte[] SaveToBinary()
        {
            var data = new List<byte>();
            data.Add(2); // Уникальный идентификатор для квадрата
            data.AddRange(BitConverter.GetBytes(A));
            return data.ToArray();
        }

        /// <summary>
        /// Загружает данные квадрата из бинарного потока.
        /// </summary>
        /// <param name="stream">Поток, из которого будут загружены данные.</param>
        public override void LoadFromBinary(FileStream stream)
        {
            var sideBytes = new byte[sizeof(double)];
            stream.Read(sideBytes, 0, sideBytes.Length);
            A = BitConverter.ToDouble(sideBytes, 0);
        }
    }
}
