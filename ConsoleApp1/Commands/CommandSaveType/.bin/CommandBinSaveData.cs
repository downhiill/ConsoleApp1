using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Commands.CommandSaveType
{
    /// <summary>
    /// Команда для сохранения данных о фигурах в бинарный файл.
    /// </summary>
    internal class CommandBinSaveData : ICommand
    {
        private readonly ShapeCollection _shapeCollection;
        private const string DefaultFileName = "ShapeData.bin"; // Имя файла по умолчанию

        /// <summary>
        /// Получает имя команды.
        /// </summary>
        public string Name => "сохранить_данные";

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CommandSaveData"/>.
        /// </summary>
        /// <param name="shapeCollection">Коллекция фигур, данные из которой будут сохранены в файл.</param>
        public CommandBinSaveData(ShapeCollection shapeCollection)
        {
            _shapeCollection = shapeCollection;
        }

        /// <summary>
        /// Выполняет команду, сохраняя данные о фигурах из коллекции в указанный бинарный файл.
        /// Если имя файла не указано, используется значение по умолчанию "ShapeData.bin".
        /// </summary>
        /// <param name="parameters">Имя файла, в который будут сохранены данные. Если параметр пустой, используется значение по умолчанию.</param>
        public void Execute(string parameters, bool shouldDisplayInfo = true)
        {
            // Используем имя файла по умолчанию, если параметр пустой
            var fileName = string.IsNullOrWhiteSpace(parameters) ? DefaultFileName : parameters;

            try
            {
                var shapes = _shapeCollection.GetAllShapes();

                // Сериализуем данные в бинарный файл
                using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, shapes.ToList());
                }

                Console.WriteLine($"Данные успешно сохранены в файл '{fileName}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Получает описание команды и её использования.
        /// </summary>
        /// <returns>Описание команды.</returns>
        public string Help()
        {
            return "Команда 'сохранить_данные' сохраняет данные о фигурах в бинарный файл.\n" +
                   "Параметры команды: имя файла для сохранения. Если имя файла не указано, используется значение по умолчанию 'ShapeData.bin'.\n" +
                   "Пример использования:\n" +
                   "сохранить_данные имя_файла.bin\n" +
                   "или\n" +
                   "сохранить_данные\n";
        }
    }
}
