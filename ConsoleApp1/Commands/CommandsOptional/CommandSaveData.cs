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
        }

        /// <summary>
        /// Форматирует данные о фигуре в строку для записи в файл.
        /// </summary>
        /// <param name="shape">Фигура, данные о которой необходимо отформатировать.</param>
        /// <returns>Строка, представляющая данные о фигуре в формате текста.</returns>
        private string FormatShapeData(Shape shape)
        {
            return shape.GetFormattedData();
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
