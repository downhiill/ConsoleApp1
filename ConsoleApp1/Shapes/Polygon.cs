using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    [Serializable]
    /// <summary>
    /// Представляет правильный многоугольник, заданный с помощью точек на плоскости.
    /// </summary>
    internal class Polygon : Shape
    {
        /// <summary>
        /// Список точек многоугольника.
        /// </summary>
        public List<Point> Points { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр правильного многоугольника с заданными точками.
        /// </summary>
        /// <param name="points">Список точек многоугольника.</param>
        public Polygon(List<Point> points)
        {
            Points = points;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Polygon"/>.
        /// </summary>
        public Polygon() { }

        /// <summary>
        /// Метод для расчета площади треугольника по его вершинам.
        /// </summary>
        /// <param name="p1">Первая точка.</param>
        /// <param name="p2">Вторая точка.</param>
        /// <param name="p3">Третья точка.</param>
        /// <returns>Площадь треугольника.</returns>
        private double CalculateTriangleArea(Point p1, Point p2, Point p3)
        {
            return Math.Abs((p1.X * (p2.Y - p3.Y) + p2.X * (p3.Y - p1.Y) + p3.X * (p1.Y - p2.Y)) / 2.0);
        }

        /// <summary>
        /// Переопределяет метод для вычисления площади многоугольника, используя координаты его вершин по формуле Гаусса.
        /// </summary>
        /// <returns>Площадь многоугольника.</returns>
        public override double S()
        {
            double area = 0;
            int n = Points.Count;

            // Простой метод разбиения многоугольника на треугольники
            // Используя первую вершину как общую для всех треугольников.
            for (int i = 1; i < n - 1; i++)
            {
                area += CalculateTriangleArea(Points[0], Points[i], Points[i + 1]);
            }

            return area;
        }

        /// <summary>
        /// Переопределяет метод для вычисления периметра многоугольника.
        /// </summary>
        /// <returns>Периметр многоугольника.</returns>
        public override double P()
        {
            double perimeter = 0;
            int n = Points.Count;

            // Суммирование длины всех сторон многоугольника.
            for (int i = 0; i < n; i++)
            {
                int j = (i + 1) % n;
                perimeter += Geometry.Distance(Points[i], Points[j]);
            }

            return perimeter;
        }

        /// <summary>
        /// Форматирует данные о многоугольнике в строку для вывода.
        /// </summary>
        /// <returns>Строка, представляющая многоугольник с его точками, периметром и площадью.</returns>
        public override string GetFormattedData() =>
            $"Фигура: Polygon, Точки: {string.Join(", ", Points.Select(p => $"({p.X};{p.Y})"))}, Периметр: {P()}, Площадь: {S()}";

        /// <summary>
        /// Возвращает команду для создания многоугольника.
        /// </summary>
        /// <returns>Строка команды для создания многоугольника.</returns>
        public override string GetCommand() =>
            $"добавить_многоугольник [{string.Join(", ", Points.Select(p => $"({p.X};{p.Y})"))}]";

        /// <summary>
        /// Сериализует данные многоугольника в массив байтов для сохранения в бинарный файл.
        /// </summary>
        /// <returns>Массив байтов, представляющий многоугольник в бинарном формате.</returns>
        public override byte[] SaveToBinary()
        {
            var data = new List<byte>();
            data.Add(5); // Уникальный идентификатор для многоугольника
            data.AddRange(BitConverter.GetBytes(Points.Count));

            // Сохранение координат каждой точки многоугольника.
            foreach (var point in Points)
            {
                data.AddRange(BitConverter.GetBytes(point.X));
                data.AddRange(BitConverter.GetBytes(point.Y));
            }

            return data.ToArray();
        }

        /// <summary>
        /// Загружает данные многоугольника из бинарного потока.
        /// </summary>
        /// <param name="stream">Поток, из которого будут загружены данные.</param>
        public override void LoadFromBinary(FileStream stream)
        {
            var pointsCountBytes = new byte[sizeof(int)];
            stream.Read(pointsCountBytes, 0, pointsCountBytes.Length);
            int pointsCount = BitConverter.ToInt32(pointsCountBytes, 0);

            Points = new List<Point>();
            // Чтение координат каждой точки многоугольника.
            for (int i = 0; i < pointsCount; i++)
            {
                var pointData = new byte[sizeof(double) * 2];
                stream.Read(pointData, 0, pointData.Length);
                double x = BitConverter.ToDouble(pointData, 0);
                double y = BitConverter.ToDouble(pointData, sizeof(double));
                Points.Add(new Point(x, y));
            }
        }
    }
}
