using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1.Commands.CommandSaveType.bin
{
    /// <summary>
    /// Команда для сохранения данных о фигурах в бинарный файл
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
        /// Инициализирует новый экземпляр класса <see cref="CommandBinSaveData"/>.
        /// </summary>
        /// <param name="shapeCollection">Коллекция фигур, данные из которой будут сохранены в файл.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="shapeCollection"/> равен null.</exception>
        public CommandBinSaveData(ShapeCollection shapeCollection)
        {
            _shapeCollection = shapeCollection ?? throw new ArgumentNullException(nameof(shapeCollection), "Коллекция фигур не может быть null");
        }

        /// <summary>
        /// Выполняет команду, сохраняя данные о фигурах в указанный файл.
        /// Если имя файла не указано, используется значение по умолчанию "ShapeData.bin".
        /// </summary>
        /// <param name="parameters">Имя файла, в который будут сохранены данные. Если параметр пустой, используется значение по умолчанию.</param>
        /// <param name="shouldDisplayInfo">Указывает, нужно ли отображать информацию об успешном выполнении команды.</param>
        public void Execute(string parameters, bool shouldDisplayInfo = true)
        {
            var fileName = string.IsNullOrWhiteSpace(parameters) ? DefaultFileName : parameters;

            try
            {
                using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                using (var writer = new BinaryWriter(stream))
                {
                    var shapes = _shapeCollection.ToList();

                    foreach (var shape in shapes)
                    {
                        // Получаем команду для создания фигуры
                        var command = shape.GetCommand(); // Получить команду создания фигуры

                        // Разделяем команду на имя и параметры
                        var parts = command.Split(new[] { ' ' }, 2);
                        var commandName = parts[0]; // Имя команды
                        var commandParams = parts.Length > 1 ? parts[1] : string.Empty; // Параметры команды

                        // Записываем имя команды и параметры в бинарном формате
                        writer.Write(commandName);
                        writer.Write(commandParams);
                    }
                }

                if (shouldDisplayInfo)
                {
                    Console.WriteLine($"Команды успешно сохранены в файл '{fileName}'.");
                }
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
            return "Команда 'сохранить_данные' сохраняет команды для создания фигур в бинарный файл.\n" +
                   "Параметры команды: имя файла для сохранения. Если имя файла не указано, используется значение по умолчанию 'ShapeData.bin'.\n" +
                   "Пример использования:\n" +
                   "сохранить_данные имя_файла.bin\n" +
                   "или\n" +
                   "сохранить_данные\n";
        }
    }
}
