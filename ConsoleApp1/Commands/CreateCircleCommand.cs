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
    public class CreateCircleCommand : ICommand
    {
        /// <summary>
        /// Получает имя команды.
        /// </summary>
        /// <value>Имя команды, используемое для её идентификации. В данном случае — "добавить_круг".</value>
        public string Name => "добавить_круг";

        /// <summary>
        /// Выполняет команду, создавая круг с заданным радиусом и добавляя его в коллекцию фигур.
        /// </summary>
        /// <param name="app">Экземпляр приложения, содержащий коллекцию фигур, в которую добавляется круг.</param>
        /// <param name="parameters">Строка параметров, содержащая радиус круга в формате [x].</param>
        public void Execute( App app, string parameters = "")
        {
            double radius = ParseRadius(parameters);
            var circle = new Circle(radius); // Создаем круг с заданным радиусом

            double area = circle.GetArea();
            double circumference = circle.GetPerimeter();

            Console.WriteLine($"Площадь круга: {area}");
            Console.WriteLine($"Длина окружности: {circumference}");

            app.ShapeCollection.Add(circle);
        }

        /// <summary>
        /// Парсит строку с параметром радиуса из строки формата [x].
        /// </summary>
        /// <param name="parameters">Строка параметров, содержащая радиус круга в формате [x].</param>
        /// <returns>Радиус круга.</returns>
        /// <exception cref="ArgumentException">Выбрасывается, если формат строки некорректен или радиус не является положительным числом.</exception>
        private double ParseRadius(string parameters)
        {
            // Регулярное выражение для извлечения радиуса из строки в формате [x]
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

    }
}
