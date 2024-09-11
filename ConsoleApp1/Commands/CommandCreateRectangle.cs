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
    /// Команда для создания и добавления прямоугольника в коллекцию.
    /// </summary>
    internal class CommandCreateRectangle : ICommand
    {
        private readonly ShapeCollection _shapeCollection;

        public CommandCreateRectangle(ShapeCollection shapeCollection)
        {
            _shapeCollection = shapeCollection;
        }

        /// <summary>
        /// Получает имя команды.
        /// </summary>
        /// <value>Имя команды, используемое для её идентификации. В данном случае — "добавить_прямоугольник".</value>
        public string Name => "добавить_прямоугольник";

        /// <summary>
        /// Выполняет команду, создавая прямоугольник с заданными шириной и высотой и добавляя его в коллекцию фигур.
        /// </summary>
        /// <param name="parameters">Строка параметров, содержащая ширину и высоту прямоугольника в формате [ширина;высота].</param>
        public void Execute(string parameters)
        {
            // Разделяем параметры на ширину и высоту
            var dimensions = ParseDimensions(parameters);
            var rectangle = new Rectangle(dimensions.Width, dimensions.Height); // Создаем прямоугольник

            double area = rectangle.S();
            double perimeter = rectangle.P();

            Console.WriteLine($"Площадь прямоугольника: {area}");
            Console.WriteLine($"Периметр прямоугольника: {perimeter}");

            _shapeCollection.Add(rectangle); // Добавляем прямоугольник в список фигур
        }

        /// <summary>
        /// Парсит строку с параметрами ширины и высоты прямоугольника из строки формата [ширина;высота].
        /// </summary>
        /// <param name="parameters">Строка параметров, содержащая ширину и высоту прямоугольника в формате [ширина;высота].</param>
        /// <returns>Кортеж с шириной и высотой прямоугольника.</returns>
        /// <exception cref="ArgumentException">Выбрасывается, если формат строки некорректен или размеры некорректны.</exception>
        private (double Width, double Height) ParseDimensions(string parameters)
        {
            // Регулярное выражение для извлечения ширины и высоты из строки в формате [ширина;высота]
            var pattern = @"\[(\d+(\.\d+)?);(\d+(\.\d+)?)\]";
            var match = Regex.Match(parameters, pattern);

            if (match.Success)
            {
                var widthStr = match.Groups[1].Value;
                var heightStr = match.Groups[3].Value;

                if (double.TryParse(widthStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double width) &&
                    double.TryParse(heightStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double height) &&
                    width > 0 && height > 0)
                {
                    return (width, height);
                }
                else
                {
                    throw new ArgumentException("Некорректные размеры. Пожалуйста, введите положительные числа для ширины и высоты.");
                }
            }
            else
            {
                throw new ArgumentException("Некорректный формат данных. Пожалуйста, используйте формат [ширина;высота], где ширина и высота — положительные числа.");
            }
        }
    }
}
