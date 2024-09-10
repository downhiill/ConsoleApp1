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
        public List<Point> Points { get; private set; }

        /// <summary>
        /// Инициализация нового экземпляра правильного многоугольника с заданными точками.
        /// </summary>
        /// <param name="points">Список точек многоугольника.</param>
        public Polygon(List<Point> points)
        {
            Points = points;
        }

        /// <summary>
        /// Метод для расчета площади треугольника
        /// </summary>
        /// <param name="p1">Первая точка</param>
        /// <param name="p2">Вторая точка</param>
        /// <param name="p3">Третья точка</param>
        /// <returns></returns>
        private double CalculateTriangleArea(Point p1, Point p2, Point p3)
        {
            return Math.Abs((p1.X * (p2.Y - p3.Y) + p2.X * (p3.Y - p1.Y) + p3.X * (p1.Y - p2.Y)) / 2.0);
        }
        /// <summary>
        /// Переопределяет метод для вычисления площади многоугольника, используя координаты его вершин по формуле Гаусса.
        /// </summary>
        /// <returns>Площадь многоугольника.</returns>
        public override double GetArea()
        {
            double area = 0;
            int n = Points.Count;

            // Простой метод разбиения многоугольника на треугольники
            // используя первую вершину как общую для всех треугольников
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
        public override double GetPerimeter()
        {
            double perimeter = 0;
            int n = Points.Count;

            for (int i = 0; i < n; i++)
            {
                int j = (i + 1) % n;
                perimeter += Geometry.Distance(Points[i], Points[j]);
            }

            return perimeter;
        }

        /// <summary>
        /// Создает новый экземпляр правильного многоугольника на основе ввода пользователя.
        /// </summary>
        /// <returns>Новый экземпляр многоугольника или <c>null</c>, если ввод неверен.</returns>
        public static Polygon CreateRegularPolygon()
        {
            List<Point> points = new List<Point>();
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

                points.Add(new Point(x, y));
            }

            // Проверяем, что многоугольник имеет как минимум 3 точки
            if (points.Count < 3)
            {
                Console.WriteLine("Для формирования многоугольника нужно как минимум 3 точки.");
                return null;
            }

            return new Polygon(points);
        }




    }
}
