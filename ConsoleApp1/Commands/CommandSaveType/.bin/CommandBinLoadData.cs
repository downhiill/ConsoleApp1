using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Commands.CommandSaveType
{
    internal class CommandBinLoadData:ICommand
    {
        private readonly ShapeCollection _shapeCollection;
        private readonly App _app;
        private const string DefaultFileName = "ShapeData.bin"; // Имя файла по умолчанию

        /// <summary>
        /// Получает имя команды.
        /// </summary>
        public string Name => "загрузить_данные";

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CommandLoadBinaryData"/>.
        /// </summary>
        /// <param name="shapeCollection">Коллекция фигур, в которую будут добавлены загруженные данные.</param>
        public CommandBinLoadData(ShapeCollection shapeCollection, App app)
        {
            _shapeCollection = shapeCollection ?? throw new ArgumentNullException(nameof(shapeCollection), "Коллекция фигур не может быть null");
            _app = app ?? throw new ArgumentNullException(nameof(app), "Экземпляр App не может быть null");
        }

        /// <summary>
        /// Выполняет команду, загружая данные о фигурах из указанного бинарного файла и добавляя их в коллекцию.
        /// Если имя файла не указано, используется значение по умолчанию "ShapeData.bin".
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
                using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    var formatter = new BinaryFormatter();
                    var shapes = (List<Shape>)formatter.Deserialize(fileStream); // Предполагается, что вы сериализуете List<Shape>

                    foreach (var shape in shapes)
                    {
                        _shapeCollection.Add(shape);
                    }
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
            return "Команда 'загрузить_данные_бинарные' загружает данные о фигурах из бинарного файла.\n" +
                   "Параметры команды: имя файла для загрузки. Если имя файла не указано, используется значение по умолчанию 'ShapeData.bin'.\n" +
                   "Пример использования:\n" +
                   "загрузить_данные_бинарные имя_файла.bin\n" +
                   "или\n" +
                   "загрузить_данные_бинарные\n";
        }
    }
}
