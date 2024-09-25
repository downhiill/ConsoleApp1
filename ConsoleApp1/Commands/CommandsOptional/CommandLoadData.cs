
using ConsoleApp1.Extensions;
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
        private readonly List<ICommand> _commands;
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
        public CommandLoadData(ShapeCollection shapeCollection, List<ICommand> commands)
        {
            _shapeCollection = shapeCollection;
            _commands = commands ?? throw new ArgumentNullException(nameof(commands), "Список команд не может быть null");
        }

        /// <summary>
        /// Выполняет команду, загружая данные о фигурах из указанного файла и добавляя их в коллекцию.
        /// Если имя файла не указано, используется значение по умолчанию "ShapeData.txt".
        /// </summary>
        /// <param name="parameters">Имя файла, из которого будут загружены данные. Если параметр пустой, используется значение по умолчанию.</param>
        public void Execute(string parameters, bool shouldDisplayInfo = true)
        {
            var fileName = string.IsNullOrWhiteSpace(parameters) ? DefaultFileName : parameters;

            if (!File.Exists(fileName))
            {
                Console.WriteLine($"Файл {fileName} не найден.");
                return;
            }

            try
            {
                var lines = File.ReadAllLines(fileName, Encoding.UTF8);

                if (lines == null || !lines.Any())
                {
                    Console.WriteLine("Файл пуст или не содержит данных.");
                    return;
                }

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
            if (string.IsNullOrWhiteSpace(line))
            {
                Console.WriteLine("Пустая или невалидная строка для разбора: " + line);
                return;
            }

            // Разделение строки на имя команды и параметры
            var parts = line.Split(new[] { ' ' }, 2);
            if (parts.Length < 1 || string.IsNullOrWhiteSpace(parts[0]))
            {
                Console.WriteLine($"Неверный формат строки команды: {line}");
                return;
            }

            var commandName = parts[0];
            var parameters = parts.Length > 1 ? parts[1] : string.Empty;

            var command = _commands.FirstOrDefault(c => c.Name == commandName);
            if (command != null)
            {
                // При загрузке данных передаем параметр false, чтобы подавить вывод
                CommandExtensions.TryExecute(command, parameters, false);
            }
            else
            {
                Console.WriteLine($"Неизвестная команда: {commandName}");
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
