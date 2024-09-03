using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// Представляет правильный многоугольник, заданный с помощью точек на плоскости.
    /// </summary>
    internal class Polygon : Shape
    {
        /// <summary>
        /// Список точек многоугольника.
        /// </summary>
        public List<PointsPolygon> Points { get; private set; }

        /// <summary>
        /// Инициализация нового экземпляра правильного многоугольника с заданными точками.
        /// </summary>
        /// <param name="points">Список точек многоугольника.</param>
        public Polygon(List<PointsPolygon> points)
        {
            Points = points;
        }

        /// <summary>
        /// Переопределяет метод для вычисления площади многоугольника, используя координаты его вершин по формуле Гаусса.
        /// </summary>
        /// <returns>Площадь многоугольника.</returns>
        public override double GetArea()
        {
            double area = 0;
            int n = Points.Count;

            for (int i = 0; i < n; i++)
            {
                int j = (i + 1) % n;
                area += Points[i].X * Points[j].Y;
                area -= Points[j].X * Points[i].Y;
            }

            area = Math.Abs(area) / 2.0;
            return area;
        }

        /// <summary>
        /// Переопределяет метод для вычисления периметра многоугольника.
        /// </summary>
        /// <returns>Периметр многоугольника.</returns>
        public override double GetPerimeter()
        {
            double perimeter = 0;
            int n = Points.Count;

            for (int i = 0; i < n; i++)
            {
                int j = (i + 1) % n;
                perimeter += Distance(Points[i], Points[j]);
            }

            return perimeter;
        }

        /// <summary>
        /// Переопределение метода для отображения информации о многоугольнике.
        /// </summary>
        public override void Display()
        {
            Console.WriteLine($"Многоугольник:");
            for (int i = 0; i < Points.Count; i++)
            {
                Console.WriteLine($"Точка {i + 1}: ({Points[i].X}, {Points[i].Y})");
            }
            Console.WriteLine($"Площадь = {GetArea():F2}");
            Console.WriteLine($"Периметр = {GetPerimeter():F2}");
        }

        /// <summary>
        /// Создает новый экземпляр правильного многоугольника на основе ввода пользователя.
        /// </summary>
        /// <returns>Новый экземпляр многоугольника или <c>null</c>, если ввод неверен.</returns>
        public static Polygon CreateRegularPolygon()
        {
            List<PointsPolygon> points = new List<PointsPolygon>();
            string input;

            Console.WriteLine("Введите точки многоугольника. Для завершения ввода введите 'done'.");

            while (true)
            {
                Console.Write("Введите координату X (или 'done' для завершения): ");
                input = Console.ReadLine();

                if (input.ToLower() == "done")
                {
                    break;
                }

                if (!int.TryParse(input, out int x))
                {
                    Console.WriteLine("Некорректное значение. Попробуйте снова.");
                    continue;
                }

                Console.Write("Введите координату Y: ");
                if (!int.TryParse(Console.ReadLine(), out int y))
                {
                    Console.WriteLine("Некорректное значение. Попробуйте снова.");
                    continue;
                }

                points.Add(new PointsPolygon(x, y));
            }

            // Проверяем, что многоугольник имеет как минимум 3 точки
            if (points.Count < 3)
            {
                Console.WriteLine("Для формирования многоугольника нужно как минимум 3 точки.");
                return null;
            }

            return new Polygon(points);
        }


        /// <summary>
        /// Вычисляет расстояние между двумя точками.
        /// </summary>
        /// <param name="p1">Первая точка.</param>
        /// <param name="p2">Вторая точка.</param>
        /// <returns>Расстояние между точками.</returns>
        private static double Distance(PointsPolygon p1, PointsPolygon p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

    }
}
