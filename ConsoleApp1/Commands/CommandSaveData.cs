using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp1.Commands
{
    internal class CommandSaveData : ICommand
    {
        private readonly ShapeCollection _shapeCollection;
        private const string DefaultFileName = "Shape.json"; // Имя файла по умолчанию

        public string Name => "сохранить_данные";

        public CommandSaveData(ShapeCollection shapeCollection)
        {
            _shapeCollection = shapeCollection;
        }

        public void Execute(string parameters)
        {
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
        }

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
