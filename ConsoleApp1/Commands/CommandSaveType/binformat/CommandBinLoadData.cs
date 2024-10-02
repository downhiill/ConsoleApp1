using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1.Commands.CommandSaveType
{
    /// <summary>
    /// Команда для загрузки данных о фигурах из бинарного ф
    /// </summary>
    internal class CommandBinLoadData : ICommand
    {
        private readonly ShapeCollection _shapeCollection;
        private readonly App _app;
        private const string DefaultFileName = "ShapeData.bin"; // Имя файла по умолчанию

        /// <summary>
        /// Получает имя команды.
        /// </summary>
        public string Name => "загрузить_данные";

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CommandBinLoadData"/>.
        /// </summary>
        /// <param name="shapeCollection">Коллекция фигур, в которую будут загружены данные из файла.</param>
        /// <param name="app">Экземпляр приложения для выполнения команд.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="shapeCollection"/> или <paramref name="app"/> равны null.</exception>
        public CommandBinLoadData(ShapeCollection shapeCollection, App app)
        {
            _shapeCollection = shapeCollection ?? throw new ArgumentNullException(nameof(shapeCollection), "Коллекция фигур не может быть null");
            _app = app ?? throw new ArgumentNullException(nameof(app), "Экземпляр App не может быть null");
        }

        /// <summary>
        /// Выполняет команду, загружая данные о фигурах из указанного файла.
        /// Если имя файла не указано, используется значение по умолчанию "ShapeData.bin".
        /// </summary>
        /// <param name="parameters">Имя файла, из которого будут загружены данные. Если параметр пустой, используется значение по умолчанию.</param>
        /// <param name="shouldDisplayInfo">Указывает, нужно ли отображать информацию об успешном выполнении команды.</param>
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
                using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                using (var reader = new BinaryReader(fileStream))
                {
                    while (fileStream.Position < fileStream.Length)
                    {
                        var commandName = reader.ReadString(); // Читаем имя команды
                        var commandParams = reader.ReadString(); // Читаем параметры команды

                        // Выполняем команду в приложении
                        _app.ExecuteCommand(commandName, commandParams);
                    }
                }

                if (shouldDisplayInfo)
                {
                    Console.WriteLine("Данные успешно загружены в коллекцию.");
                }
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
            return "Команда 'загрузить_данные' загружает данные о фигурах из бинарного файла.\n" +
                   "Параметры команды: имя файла для загрузки. Если имя файла не указано, используется значение по умолчанию 'ShapeData.bin'.\n" +
                   "Пример использования:\n" +
                   "загрузить_данные имя_файла.bin\n" +
                   "или\n" +
                   "загрузить_данные\n";
        }
    }
}
