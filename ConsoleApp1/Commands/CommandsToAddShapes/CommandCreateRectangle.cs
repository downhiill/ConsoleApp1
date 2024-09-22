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
    /// Команда для создания и добавления прямоугольника в коллекцию.
    /// </summary>
    internal class CommandCreateRectangle : ICommand
    {
        private readonly ShapeCollection _shapeCollection;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandCreateRectangle"/> с указанной коллекцией фигур.
        /// </summary>
        /// <param name="shapeCollection">Коллекция фигур, в которую будет добавлен новый многоугольник.</param>
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

            Console.WriteLine($"Площадь прямоугольника: {rectangle.S()}");
            Console.WriteLine($"Периметр прямоугольника: {rectangle.P()}");

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

        /// <summary>
        /// Создает объект <see cref="Rectangle"/> из строки, содержащей информацию о прямоугольнике.
        /// </summary>
        /// <param name="data">Строка данных, содержащая информацию о прямоугольнике в формате, где указаны значения ширины и высоты, например "Ширина: 10.0, Высота: 5.0".</param>
        /// <returns>Объект <see cref="Rectangle"/>, созданный на основе данных из строки.</returns>
        /// <exception cref="FormatException">Выбрасывается, если строка не содержит корректных значений ширины и высоты.</exception>
        public static Rectangle FromString(string data)
        {
            var widthPart = data.Split(',').FirstOrDefault(p => p.Contains("Ширина:"));
            var heightPart = data.Split(',').FirstOrDefault(p => p.Contains("Высота:"));
            if (widthPart != null && heightPart != null)
            {
                var width = ExtractValue(widthPart);
                var height = ExtractValue(heightPart);
                if (double.TryParse(width, out double w) && double.TryParse(height, out double h))
                {
                    return new Rectangle(w, h);
                }
            }
            throw new FormatException("Неверный формат данных для Rectangle");
        }

        /// <summary>
        /// Извлекает значение из строки в формате "Ключ: Значение".
        /// </summary>
        /// <param name="part">Строка, содержащая ключ и значение, разделенные двоеточием, например "Ширина: 10.0".</param>
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
            return "Команда 'добавить_прямоугольник' создает прямоугольник с заданными шириной и высотой и добавляет его в коллекцию фигур.\n" +
                   "Формат параметров: [ширина;высота], где ширина и высота — положительные числа.\n" +
                   "Примеры использования:\n" +
                   "добавить_прямоугольник [5.0;10.0]\n" +
                   "добавить_прямоугольник [3;7]\n";
        }
    }
}
