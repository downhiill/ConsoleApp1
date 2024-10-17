using ConsoleApp1.Commands.CommandSaveType.txt;
using System;
using ConsoleApp1.Infrastructure;

namespace ConsoleApp1.Commands
{
    /// <summary>
    /// Команда для сохранения данных о фигурах в файл (текстовый или бинарный).
    /// </summary>
    internal class CommandSaveData : ICommand
    {
        private readonly IShapeCollection _shapeCollection;
        private const string DefaultTxtFileName = "ShapeData.txt"; // Имя файла по умолчанию для текстового формата
        private const string DefaultBinFileName = "ShapeData.bin"; // Имя файла по умолчанию для бинарного формата

        /// <summary>
        /// Возвращает имя команды.
        /// </summary>
        public string Name => "сохранить_данные";

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CommandSaveData"/>.
        /// </summary>
        /// <param name="shapeCollection">Коллекция фигур, которую нужно сохранить.</param>
        public CommandSaveData(IShapeCollection shapeCollection)
        {
            _shapeCollection = shapeCollection ?? throw new ArgumentNullException(nameof(shapeCollection), "Коллекция фигур не может быть null");
        }

        /// <summary>
        /// Выполняет команду сохранения данных о фигурах в указанный файл.
        /// </summary>
        /// <param name="parameters">Имя файла для сохранения. Если параметр пустой, используется имя по умолчанию.</param>
        /// <param name="shouldDisplayInfo">Указывает, нужно ли отображать информацию об успешном сохранении.</param>
        public void Execute(string parameters, bool shouldDisplayInfo = true)
        {
            // Выбор файла по умолчанию, если параметр не указан
            var fileName = string.IsNullOrWhiteSpace(parameters) ? DefaultTxtFileName : parameters;

            // Определение типа команды в зависимости от расширения файла
            ICommand commandToExecute;

            if (fileName.EndsWith(".bin", StringComparison.OrdinalIgnoreCase))
            {
                commandToExecute = new CommandBinSaveData(_shapeCollection); // Сохранение данных в бинарный файл
            }
            else // По умолчанию сохраняем в текстовый файл
            {
                commandToExecute = new CommandTxtSaveData(_shapeCollection); // Сохранение данных в текстовый файл
            }

            // Выполнение соответствующей команды
            commandToExecute.Execute(fileName, shouldDisplayInfo);
        }

        /// <summary>
        /// Возвращает описание команды и её использования.
        /// </summary>
        /// <returns>Описание команды.</returns>
        public string Help()
        {
            return "Команда 'сохранить_данные' сохраняет данные о фигурах в файл.\n" +
                   "Параметры команды: имя файла для сохранения с расширением (.txt или .bin). Если имя файла не указано, используется значение по умолчанию 'ShapeData.txt'.\n" +
                   "Пример использования:\n" +
                   "сохранить_данные имя_файла.txt\n" +
                   "или\n" +
                   "сохранить_данные имя_файла.bin\n" +
                   "или\n" +
                   "сохранить_данные\n";
        }
    }
}
