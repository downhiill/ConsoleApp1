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
    /// Команда для создания и добавления круга в коллекцию.
    /// </summary>
    public class CommandCreateCircle : ICommand
    {
        private readonly ShapeCollection _shapeCollection;

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
        public void Execute(string parameters)
        {
            double radius = ParseRadius(parameters);
            var circle = new Circle(radius); // Создаем круг с заданным радиусом

            Console.WriteLine($"Площадь круга: {circle.S()}");
            Console.WriteLine($"Длина окружности: {circle.P()}");

            _shapeCollection.Add(circle);
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
