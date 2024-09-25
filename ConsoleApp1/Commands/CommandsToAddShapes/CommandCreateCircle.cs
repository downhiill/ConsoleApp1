using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1.CommandsToAddShapes
{
    /// <summary>
    /// Команда для создания и добавления круга в коллекцию.
    /// </summary>
    public class CommandCreateCircle : ICommand
    {
        private readonly ShapeCollection _shapeCollection;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandCreateCircle"/> с указанной коллекцией фигур.
        /// </summary>
        /// <param name="shapeCollection">Коллекция фигур, в которую будет добавлен новый многоугольник.</param>
        public CommandCreateCircle(ShapeCollection shapeCollection)
        {
            _shapeCollection = shapeCollection;
        }

        /// <summary>
        /// Получает имя команды.
        /// </summary>
        /// <value>Имя команды, используемое для её идентификации. В данном случае — "добавить_круг".</value>
        public string Name => "добавить_круг";

        /// <summary>
        /// Выполняет команду, создавая круг с заданным радиусом и добавляя его в коллекцию фигур.
        /// </summary>
        /// <param name="parameters">Строка параметров, содержащая радиус круга в формате [x].</param>
        public void Execute(string parameters, bool shouldDisplayInfo = true)
        {
            double radius = ParseRadius(parameters);
            var circle = new Circle(radius); // Создаем круг с заданным радиусом
            _shapeCollection.Add(circle);

            if(shouldDisplayInfo)
            {
                Console.WriteLine($"Площадь круга: {circle.S()}");
                Console.WriteLine($"Длина окружности: {circle.P()}");
            }  
        }

        /// <summary>
        /// Парсит строку с параметром радиуса из строки формата [x].
        /// </summary>
        /// <param name="parameters">Строка параметров, содержащая радиус круга в формате [x].</param>
        /// <returns>Радиус круга.</returns>
        /// <exception cref="ArgumentException">Выбрасывается, если формат строки некорректен или радиус не является положительным числом.</exception>
        double ParseRadius(string parameters)
        {
            var pattern = @"\[(.*?)\]";
            var match = Regex.Match(parameters, pattern);

            if (match.Success)
            {
                var radiusStr = match.Groups[1].Value;
                if (double.TryParse(radiusStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double radius) && radius > 0)
                {
                    return radius;
                }
                else
                {
                    throw new ArgumentException("Некорректный радиус. Пожалуйста, введите положительное число.");
                }
            }
            else
            {
                throw new ArgumentException("Некорректный формат данных. Пожалуйста, используйте формат [x], где x — радиус круга.");
            }
        }

        /// <summary>
        /// Создает объект <see cref="Circle"/> из строки, содержащей информацию о круге.
        /// </summary>
        /// <param name="data">Строка данных, содержащая информацию о круге в формате, где указано значение радиуса, например "Радиус: 5.0".</param>
        /// <returns>Объект <see cref="Circle"/>, созданный на основе данных из строки.</returns>
        /// <exception cref="FormatException">Выбрасывается, если строка не содержит корректного значения радиуса.</exception>
        public static Circle FromString(string data)
        {
            var radiusPart = data.Split(',').FirstOrDefault(p => p.Contains("Радиус:"));
            if (radiusPart != null)
            {
                var radius = ExtractValue(radiusPart);
                if (double.TryParse(radius, out double r))
                {
                    return new Circle(r);
                }
            }
            throw new FormatException("Неверный формат данных для Circle");
        }

        /// <summary>
        /// Извлекает значение из строки в формате "Ключ: Значение".
        /// </summary>
        /// <param name="part">Строка, содержащая ключ и значение, разделенные двоеточием, например "Радиус: 5.0".</param>
        /// <returns>Значение после двоеточия, удаляя ведущие и завершающие пробелы.</returns>
        private static string ExtractValue(string part)
        {
            var parts = part.Split(':');
            return parts.Length > 1 ? parts[1].Trim() : string.Empty;
        }

        /// <summary>
        /// Получает описание команды и её использования.
        /// </summary>
        /// <returns>Описание команды.</returns>
        public string Help()
        {
            return "Команда 'добавить_круг' создает круг с заданным радиусом и добавляет его в коллекцию фигур.\n" +
                   "Формат параметров: [x], где x — радиус круга, положительное число.\n" +
                   "Примеры использования:\n" +
                   "добавить_круг [5.5]\n" +
                   "добавить_круг [10]\n";
        }

    }
}
