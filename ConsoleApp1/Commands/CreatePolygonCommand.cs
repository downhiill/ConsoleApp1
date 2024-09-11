
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1.GeometricShapeCalculator.Infrastructure
{
    /// <summary>
    /// Команда для создания и добавления многоугольника в коллекцию.
    /// </summary>
    internal class CreatePolygonCommand : ICommand
    {
        /// <summary>
        /// Список фигур. Не используется в данной реализации.
        /// </summary>
        public List<Shape> shapes = new List<Shape>();

        /// <summary>
        /// Получает имя команды.
        /// </summary>
        /// <value>Имя команды, используемое для её идентификации. В данном случае — "добавить_многоугольник".</value>
        public string Name => "добавить_многоугольник";

        /// <summary>
        /// Выполняет команду, создавая многоугольник с заданными вершинами и добавляя его в коллекцию фигур.
        /// </summary>
        /// <param name="app">Экземпляр приложения, содержащий коллекцию фигур, в которую добавляется многоугольник.</param>
        /// <param name="parameters">Строка параметров, содержащая вершины многоугольника в формате [(x;y),(x;y),...].</param>
        public void Execute(App app, string parameters = "" )
        {
            var points = ParsePoints(parameters);
            var polygon = new Polygon(points);

            double area = polygon.GetArea();
            double perimeter = polygon.GetPerimeter();

            Console.WriteLine($"Площадь многоугольника: {area}");
            Console.WriteLine($"Периметр многоугольника: {perimeter}");

            app.ShapeCollection.Add(polygon); // Добавляем многоугольник в список фигур
        }
        /// <summary>
        /// Парсит строку с параметрами вершин многоугольника из строки формата [(x;y),(x;y),...].
        /// </summary>
        /// <param name="data">Строка параметров, содержащая вершины многоугольника в формате [(x;y),(x;y),...].</param>
        /// <returns>Список точек, представляющих вершины многоугольника.</returns>
        /// <exception cref="ArgumentException">Выбрасывается, если формат строки некорректен или количество точек меньше трёх.</exception>
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
