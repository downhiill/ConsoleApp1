using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Commands.CommandSaveType.txt
{
    internal class CommandTxtLoadData : ICommand
    {

        private readonly ShapeCollection _shapeCollection;
        private readonly App _app;
        private const string DefaultFileName = "ShapeData.txt"; // Имя файла по умолчанию

        /// <summary>
        /// Получает имя команды.
        /// </summary>
        /// <value>Имя команды, используемое для её идентификации. В данном случае — "загрузить_данные".</value>
        public string Name => "загрузить_данные";

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CommandTxtLoadData"/>.
        /// </summary>
        /// <param name="shapeCollection">Коллекция фигур, в которую будут добавлены загруженные данные.</param>
        public CommandTxtLoadData(ShapeCollection shapeCollection, App app)
        {
            _shapeCollection = shapeCollection;
            _app = app ?? throw new ArgumentNullException(nameof(app), "Экземпляр App не может быть null");
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

                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var parts = line.Split(new[] { ' ' }, 2);
                    var commandName = parts[0];
                    var commandParams = parts.Length > 1 ? parts[1] : string.Empty;

                    _app.ExecuteCommand(commandName, commandParams);
                }

                Console.WriteLine("Данные успешно загружены в коллекцию.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
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
                   "сохранить_данные имя_файла.формат файла (.txt;.bin;.json)\n" +
                   "или\n" +
                   "загрузить_данные\n";
        }
    }
}
