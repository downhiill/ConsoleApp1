using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    [Serializable]
    /// <summary>
    /// Представляет треугольник с заданными сторонами.
    /// </summary>
    internal class Triangle : Shape
    {
        /// <summary>
        /// Сторона A 
        /// </summary>
        public double A { get; set; }

        /// <summary>
        /// Сторона B 
        /// </summary>
        public double B { get; set; }

        /// <summary>
        /// Сторона C 
        /// </summary>
        public double C { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр треугольника с указанными длинами сторон.
        /// </summary>
        /// <param name="a">Сторона a.</param>
        /// <param name="b">Сторона b.</param>
        /// <param name="c">Сторона c.</param>
        public Triangle(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Triangle"/>.
        /// </summary>
        public Triangle() { }

        /// <summary>
        /// Переопределяет метод для вычисления площади треугольника по формуле Герона.
        /// </summary>
        /// <returns>Площадь треугольника.</returns>
        public override double S()
        {
            double s = P() / 2;
            double area = Math.Sqrt(s * (s - A) * (s - B) * (s - C));

            return double.IsNaN(area) || area < 0 ? 0 : area;
        }

        /// <summary>
        /// Переопределяет метод для вычисления периметра треугольника.
        /// </summary>
        /// <returns>Периметр треугольника.</returns>
        public override double P() => A + B + C;

        /// <summary>
        /// Форматирует данные о треугольнике в строку для вывода.
        /// </summary>
        /// <returns>Строка, представляющая данные о треугольнике.</returns>
        /// <remarks>
        /// Строка содержит информацию о сторонах, периметре и площади треугольника.
        /// </remarks>
        public override string GetFormattedData() =>
            $"Фигура: Triangle, Стороны: A={A}, B={B}, C={C}, Периметр: {P()}, Площадь: {S()}";

        /// <summary>
        /// Возвращает команду для создания треугольника.
        /// </summary>
        /// <returns>Строка команды для создания треугольника.</returns>
        public override string GetCommand() =>
            $"добавить_треугольник [{A};{B};{C}]";

        /// <summary>
        /// Сериализует данные треугольника в массив байтов для сохранения в бинарный файл.
        /// </summary>
        /// <returns>Массив байтов, представляющий треугольник в бинарном формате.</returns>
        public override byte[] SaveToBinary()
        {
            var data = new List<byte> { 3 }; // Уникальный идентификатор для треугольника
            data.AddRange(BitConverter.GetBytes(A));
            data.AddRange(BitConverter.GetBytes(B));
            data.AddRange(BitConverter.GetBytes(C));
            return data.ToArray();
        }

        /// <summary>
        /// Загружает данные треугольника из бинарного потока.
        /// </summary>
        /// <param name="stream">Поток, из которого будут загружены данные.</param>
        public override void LoadFromBinary(FileStream stream)
        {
            var sideBytes = new byte[sizeof(double) * 3];
            stream.Read(sideBytes, 0, sideBytes.Length);
            A = BitConverter.ToDouble(sideBytes, 0);
            B = BitConverter.ToDouble(sideBytes, sizeof(double));
            C = BitConverter.ToDouble(sideBytes, sizeof(double) * 2);
        }
    }
}
