
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1.Commands
{
    /// <summary>
    /// Команда для загрузки данных о фигурах из файла и добавления их в коллекцию.
    /// </summary>
    internal class CommandLoadData : ICommand
    {
        private readonly ShapeCollection _shapeCollection;
        private const string DefaultFileName = "ShapeData.txt"; // Имя файла по умолчанию

        /// <summary>
        /// Получает имя команды.
        /// </summary>
        /// <value>Имя команды, используемое для её идентификации. В данном случае — "загрузить_данные".</value>
        public string Name => "загрузить_данные";

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CommandLoadData"/>.
        /// </summary>
        /// <param name="shapeCollection">Коллекция фигур, в которую будут добавлены загруженные данные.</param>
        public CommandLoadData(ShapeCollection shapeCollection)
        {
            _shapeCollection = shapeCollection;
        }

        /// <summary>
        /// Выполняет команду, загружая данные о фигурах из указанного файла и добавляя их в коллекцию.
        /// Если имя файла не указано, используется значение по умолчанию "ShapeData.txt".
        /// </summary>
        /// <param name="parameters">Имя файла, из которого будут загружены данные. Если параметр пустой, используется значение по умолчанию.</param>
        public void Execute(string parameters)
        {
            // Используем имя файла по умолчанию, если параметр пустой
            var fileName = string.IsNullOrWhiteSpace(parameters) ? DefaultFileName : parameters;

            if (!File.Exists(fileName))
            {
                Console.WriteLine($"Файл {fileName} не найден.");
                return;
            }

            try
            {
                // Чтение содержимого файла построчно
                var lines = File.ReadAllLines(fileName, Encoding.UTF8);

                lines.ToList().ForEach(line => ParseAndAddShape(line));

                Console.WriteLine("Данные успешно загружены в коллекцию.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
            }

        }

        /// <summary>
        /// Разбирает строку и добавляет соответствующую фигуру в коллекцию.
        /// </summary>
        /// <param name="line">Строка, содержащая данные о фигуре.</param>
        public void ParseAndAddShape(string line)
        {
            var parts = line.Split(',')
        .Select(part => part.Trim())
        .ToArray();

            if (parts.Length < 2)
            {
                Console.WriteLine($"Неверный формат строки: {line}");
                return;
            }

            var shapeType = parts[0].Split(':').Last().Trim();

            try
            {
                // Указываем полное пространство имен + имя класса
                var namespacePrefix = "ConsoleApp1.CommandsToAddShapes";  // Замените на ваше пространство имен
                var className = $"{namespacePrefix}.CommandCreate{shapeType}";
                var type = Type.GetType(className);

                if (type == null)
                {
                    throw new FormatException($"Неизвестная фигура: {shapeType}");
                }

                // Ищем статический метод FromString
                var method = type.GetMethod("FromString", BindingFlags.Static | BindingFlags.Public);

                if (method == null)
                {
                    throw new FormatException($"Метод FromString не найден для {shapeType}");
                }

                // Вызов метода для создания фигуры
                var shape = (Shape)method.Invoke(null, new object[] { line });
                _shapeCollection.Add(shape);
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Ошибка при разборе {shapeType}: {ex.Message}");
            }
        }

        /// <summary>
        /// Получает описание команды и её использования.
        /// </summary>
        /// <returns>Описание команды.</returns>
        public string Help()
        {
            return "Команда 'загрузить_данные' загружает данные о фигурах из файла.\n" +
                   "Параметры команды: имя файла для загрузки. Если имя файла не указано, используется значение по умолчанию 'default_shapes_data.json'.\n" +
                   "Пример использования:\n" +
                   "загрузить_данные имя_файла.json\n" +
                   "или\n" +
                   "загрузить_данные\n";
        }
    }
}
