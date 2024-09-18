using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Commands
{
    /// <summary>
    /// Команда для сохранения данных о фигурах в файл.
    /// </summary>
    internal class CommandSaveData : ICommand
    {
        private readonly ShapeCollection _shapeCollection;
        private const string DefaultFileName = "ShapeData.txt"; // Имя файла по умолчанию

        /// <summary>
        /// Получает имя команды.
        /// </summary>
        /// <value>Имя команды, используемое для её идентификации. В данном случае — "сохранить_данные".</value>
        public string Name => "сохранить_данные";

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CommandSaveData"/>.
        /// </summary>
        /// <param name="shapeCollection">Коллекция фигур, данные из которой будут сохранены в файл.</param>
        public CommandSaveData(ShapeCollection shapeCollection)
        {
            _shapeCollection = shapeCollection;
        }

        /// <summary>
        /// Выполняет команду, сохраняя данные о фигурах из коллекции в указанный файл.
        /// Если имя файла не указано, используется значение по умолчанию "ShapeData.txt".
        /// </summary>
        /// <param name="parameters">Имя файла, в который будут сохранены данные. Если параметр пустой, используется значение по умолчанию.</param>
        public void Execute(string parameters)
        {
            // Используем имя файла по умолчанию, если параметр пустой
            var fileName = string.IsNullOrWhiteSpace(parameters) ? DefaultFileName : parameters;

            try
            {
                var shapes = _shapeCollection.GetAllShapes();

                // Открываем файл в режиме добавления (append)
                using (var writer = new StreamWriter(fileName, true, Encoding.UTF8))
                {
                     shapes
                    .Select(FormatShapeData)  
                    .ToList()                 
                    .ForEach(writer.WriteLine);
                }

                Console.WriteLine($"Данные успешно сохранены в файл '{fileName}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
            }
            /*SaveJSON
            var fileName = string.IsNullOrEmpty(parameters) ? DefaultFileName : parameters;

            try
            {
                var shapes = _shapeCollection.GetAllShapes();
                List<Shape> existingShapes = new List<Shape>();
                if (File.Exists(fileName))
                {
                    var existingJson = File.ReadAllText(fileName, Encoding.UTF8);
                    existingShapes = JsonConvert.DeserializeObject<List<Shape>>(existingJson, new JsonSerializerSettings
                    {
                        Converters = { new ShapeJsonConverter() }
                    }) ?? new List<Shape>();
                }

                // Сериализация фигур в JSON
                // Объединяем старые данные с новыми
                existingShapes.AddRange(shapes);

                // Сериализация всех фигур (старые + новые) в JSON
                var jsonSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    Converters = { new ShapeJsonConverter() }
                };
                var json = JsonConvert.SerializeObject(existingShapes, jsonSettings);

                File.WriteAllText(fileName, json, Encoding.UTF8);

                Console.WriteLine($"Данные успешно сохранены в файл '{fileName}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при сохранении данных: {ex.Message}");
            }
            */
        }

        /// <summary>
        /// Форматирует данные о фигуре в строку для записи в файл.
        /// </summary>
        /// <param name="shape">Фигура, данные о которой необходимо отформатировать.</param>
        /// <returns>Строка, представляющая данные о фигуре в формате текста.</returns>
        private string FormatShapeData(Shape shape)
        {
            switch (shape)
            {
                case Circle circle:
                    return $"Фигура: Circle, Радиус: {circle.Radius}, Периметр: {circle.P()}, Площадь: {circle.S()}";
                case Square square:
                    return $"Фигура: Square, Сторона: {square.A}, Периметр: {square.P()}, Площадь: {square.S()}";
                case Rectangle rectangle:
                    return $"Фигура: Rectangle, Ширина: {rectangle.Width}, Высота: {rectangle.Height}, Периметр: {rectangle.P()}, Площадь: {rectangle.S()}";
                case Triangle triangle:
                    return $"Фигура: Triangle, Стороны: A={triangle.A}, B={triangle.B}, C={triangle.C}, Периметр: {triangle.P()}, Площадь: {triangle.S()}";
                case Polygon polygon:
                    var points = string.Join(", ", polygon.Points.Select(p => $"({p.X};{p.Y})"));
                    return $"Фигура: Polygon, Точки: {points}, Периметр: {polygon.P()}, Площадь: {polygon.S()}";
                default:
                    return "Неизвестная фигура";
            }
        }

        /// <summary>
        /// Получает описание команды и её использования.
        /// </summary>
        /// <returns>Описание команды.</returns>
        public string Help()
        {
            return "Команда 'сохранить_данные' сохраняет данные о фигурах в файл в формате JSON.\n" +
                   "Параметры команды: имя файла для сохранения. Если имя файла не указано, используется значение по умолчанию 'Shape.json'.\n" +
                   "Пример использования:\n" +
                   "сохранить_данные имя_файла.json\n" +
                   "или\n" +
                   "сохранить_данные\n";
        }
    }
}
