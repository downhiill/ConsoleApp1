
using ConsoleApp1.Commands.CommandSaveType.txt;
using ConsoleApp1.Commands.CommandSaveType;
using System;

namespace ConsoleApp1.Commands
{
    /// <summary>
    /// Команда для загрузки данных о фигурах из файла и добавления их в коллекцию.
    /// </summary>
    internal class CommandLoadData : ICommand
    {
        private readonly ShapeCollection _shapeCollection;
        private readonly App _app;
        private const string DefaultTxtFileName = "ShapeData.txt";
        private const string DefaultBinFileName = "ShapeData.bin";

        public string Name => "загрузить_данные";

        public CommandLoadData(ShapeCollection shapeCollection, App app)
        {
            _shapeCollection = shapeCollection ?? throw new ArgumentNullException(nameof(shapeCollection), "Коллекция фигур не может быть null");
            _app = app ?? throw new ArgumentNullException(nameof(app), "Экземпляр App не может быть null");
        }

        public void Execute(string parameters, bool shouldDisplayInfo = true)
        {
            var fileName = string.IsNullOrWhiteSpace(parameters) ? DefaultTxtFileName : parameters;

            ICommand commandToExecute;

            if (fileName.EndsWith(".bin", StringComparison.OrdinalIgnoreCase))
            {
                commandToExecute = new CommandBinLoadData(_shapeCollection);
            }
            else // По умолчанию загружаем из txt
            {
                commandToExecute = new CommandTxtLoadData(_shapeCollection, _app);
            }

            commandToExecute.Execute(fileName, shouldDisplayInfo);
        }

        public string Help()
        {
            return "Команда 'загрузить_данные' загружает данные о фигурах из файла.\n" +
                   "Параметры команды: имя файла для загрузки с расширением (.txt или .bin). Если имя файла не указано, используется значение по умолчанию 'ShapeData.txt'.\n" +
                   "Пример использования:\n" +
                   "загрузить_данные имя_файла.txt\n" +
                   "или\n" +
                   "загрузить_данные имя_файла.bin\n" +
                   "или\n" +
                   "загрузить_данные\n";
        }
    }
}
