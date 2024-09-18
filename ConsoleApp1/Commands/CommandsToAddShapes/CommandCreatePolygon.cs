
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
    internal class CommandCreatePolygon : ICommand
    {
        private readonly ShapeCollection _shapeCollection;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandCreatePolygon"/> с указанной коллекцией фигур.
        /// </summary>
        /// <param name="shapeCollection">Коллекция фигур, в которую будет добавлен новый многоугольник.</param>
        public CommandCreatePolygon(ShapeCollection shapeCollection)
        {
            _shapeCollection = shapeCollection;
        }

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
        /// <param name="parameters">Строка параметров, содержащая вершины многоугольника в формате [(x;y),(x;y),...].</param>
        public void Execute(string parameters)
        {
            var points = WriteParsePoints(parameters);
            var polygon = new Polygon(points);

            Console.WriteLine($"Площадь многоугольника: {polygon.S()}");
            Console.WriteLine($"Периметр многоугольника: {polygon.P()}");

            _shapeCollection.Add(polygon); // Добавляем многоугольник в список фигур
        }

        /// <summary>
        /// Парсит записываемую строку с параметрами вершин многоугольника из строки формата [(x;y),(x;y),...].
        /// </summary>
        /// <param name="data">Строка параметров, содержащая вершины многоугольника в формате [(x;y),(x;y),...].</param>
        /// <returns>Список точек, представляющих вершины многоугольника.</returns>
        /// <exception cref="ArgumentException">Выбрасывается, если формат строки некорректен или количество точек меньше трёх.</exception>
        private List<Point> WriteParsePoints(string data)
        {
            var points = new List<Point>();
            var pattern = @"\[(.*?)\]";
            var match = Regex.Match(data, pattern);

            if (match.Success)
            {
                var pointsStr = match.Groups[1].Value;
                var pointPattern = @"\((\d+(\.\d+)?);(\d+(\.\d+)?)\)";
                var matches = Regex.Matches(pointsStr, pointPattern);

                if (matches.Count < 3)
                {
                    throw new ArgumentException("Для многоугольника требуется минимум три точки.");
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
                throw new ArgumentException("Некорректный формат данных. Пожалуйста, используйте формат [(x;y),(x;y),...], " +
                "где x — первая координата; y — вторая координата (минимум 3 точки).");
            }

            return points;
        }

        /// <summary>
        /// Создаёт экземпляр <see cref="Polygon"/> из строки, содержащей описание многоугольника.
        /// </summary>
        /// <param name="data">Строка, содержащая информацию о многоугольнике, в формате, где "Точки:" указывает на начало списка вершин, а "Периметр:" — на конец списка.</param>
        /// <returns>Экземпляр <see cref="Polygon"/>, созданный на основе указанных вершин.</returns>
        /// <exception cref="FormatException">Выбрасывается, если формат строки некорректен или если количество вершин меньше трёх.</exception>
        internal static Polygon FromString(string data)
        {
            var pointsStartIndex = data.IndexOf("Точки:") + "Точки:".Length;
            var pointsString = data.Substring(pointsStartIndex)
                .Split(new[] { "Периметр:" }, StringSplitOptions.None)
                .FirstOrDefault()
                ?.Trim()
                .TrimEnd(',');

            // Разбираем строку с точками
            var points = ReadParsePoints(pointsString);

            if (points != null && points.Count >= 3)
            {
                return new Polygon(points);
            }

            throw new FormatException("Неверный формат данных для Polygon");
        }

        /// <summary>
        /// Разбирает строку, содержащую список точек, и преобразует её в список объектов <see cref="Point"/>.
        /// </summary>
        /// <param name="pointsString">Строка, содержащая список точек в формате "(x;y),(x;y),...". Точки разделены запятыми, а координаты точек — точкой с запятой.</param>
        /// <returns>Список объектов <see cref="Point"/>, представляющих разобранные точки.</returns>
        /// <exception cref="FormatException">Выбрасывается, если формат точки в строке некорректен.</exception>
        private static List<Point> ReadParsePoints(string pointsString)
        {
             var points = pointsString
            .Trim('(', ')')
            .Split(new[] { "),(" }, StringSplitOptions.RemoveEmptyEntries)
            .Select(pointString => pointString.Split(';'))
            .Where(coordinates => coordinates.Length == 2 &&
                                  double.TryParse(coordinates[0], out _) &&
                                  double.TryParse(coordinates[1], out _))
            .Select(coordinates => new Point(double.Parse(coordinates[0]), double.Parse(coordinates[1])))
            .ToList();

            if (points.Count == 0)
            {
                Console.WriteLine("Ни одна точка не была разобрана.");
            }

            return points;
        }

        /// <summary>
        /// Получает описание команды и её использования.
        /// </summary>
        /// <returns>Описание команды.</returns>
        public string Help()
        {
            return "Команда 'добавить_многоугольник' создает многоугольник с заданными вершинами и добавляет его в коллекцию фигур.\n" +
                   "Формат параметров: [(x;y),(x;y),...], где x и y — координаты вершин многоугольника. \n" +
                   "Минимальное количество вершин — 3.\n" +
                   "Примеры использования:\n" +
                   "добавить_многоугольник [(1;1),(1;4),(4;4),(4;1)]\n" +
                   "добавить_многоугольник [(0;0),(5;0),(5;5),(0;5)]\n";
        }

    }
}
