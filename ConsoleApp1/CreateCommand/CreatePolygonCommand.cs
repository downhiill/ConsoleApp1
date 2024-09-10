
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1.GeometricShapeCalculator.Infrastructure
{
    internal class CreatePolygonCommand : IApp
    {
        public string Name => "добавить_многоугольник";
        public void Execute(App app, string parameters = "" )
        {
            var points = ParsePoints(parameters);
            var polygon = new Polygon(points);

            double area = polygon.GetArea();
            double perimeter = polygon.GetPerimeter();

            Console.WriteLine($"Площадь многоугольника: {area}");
            Console.WriteLine($"Периметр многоугольника: {perimeter}");

            app.Add(polygon); // Добавляем многоугольник в список фигур
        }
        private List<Point> ParsePoints(string data)
        {
            var points = new List<Point>();
            var pattern = @"\[(.*?)\]";
            var match = Regex.Match(data, pattern);

            if (match.Success)
            {
               
                var pointsStr = match.Groups[1].Value;
                var pointPattern = @"\((\d+(\.\d+)?);(\d+(\.\d+)?)\)";
                var matches = Regex.Matches(pointsStr, pointPattern);
                if (matches.Count != 3)
                {
                    throw new ArgumentException("Для треугольника требуется три точки.");
                }
                foreach (Match m in matches)
                {
                    double x = double.Parse(m.Groups[1].Value, CultureInfo.InvariantCulture);
                    double y = double.Parse(m.Groups[3].Value, CultureInfo.InvariantCulture);
                    points.Add(new Point(x, y));
                }
                
            }
            else
            {
                throw new ArgumentException("Некорректный формат данных. Пожалуйста, используйте формат [(x;y),(x;y),(x;y)] " +
                "\nгде x — первая координата; y - вторая координата (минимальное количество точек 3) ");
            }

            return points;
        }

    }
}
