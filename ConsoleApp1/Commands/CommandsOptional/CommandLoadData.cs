using ConsoleApp1.Commands.CommandSaveType;
using ConsoleApp1.Commands.CommandSaveType.txt;
using System;

namespace ConsoleApp1.Commands
{
    /// <summary>
    /// Команда для загрузки данных о фигурах из текстового или бинарного файла и добавления их в коллекцию.
    /// </summary>
    internal class CommandLoadData : ICommand
    {
        private readonly ShapeCollection _shapeCollection;
        private readonly App _app;
        private const string DefaultTxtFileName = "ShapeData.txt"; // Имя файла по умолчанию для текстового формата
        private const string DefaultBinFileName = "ShapeData.bin"; // Имя файла по умолчанию для бинарного формата

        /// <summary>
        /// Возвращает имя команды.
        /// </summary>
        public string Name => "загрузить_данные";

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CommandLoadData"/>.
        /// </summary>
        /// <param name="shapeCollection">Коллекция фигур, в которую будут загружены данные из файла.</param>
        /// <param name="app">Экземпляр <see cref="App"/>, через который будут выполняться команды.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если переданные коллекция или экземпляр App равны null.</exception>
        public CommandLoadData(ShapeCollection shapeCollection, App app)
        {
            _shapeCollection = shapeCollection ?? throw new ArgumentNullException(nameof(shapeCollection), "Коллекция фигур не может быть null");
            _app = app ?? throw new ArgumentNullException(nameof(app), "Экземпляр App не может быть null");
        }

        /// <summary>
        /// Выполняет команду загрузки данных о фигурах из указанного файла.
        /// </summary>
        /// <param name="parameters">Имя файла для загрузки. Если параметр пустой, используется имя по умолчанию.</param>
        /// <param name="shouldDisplayInfo">Указывает, нужно ли отображать информацию об успешной загрузке.</param>
        public void Execute(string parameters, bool shouldDisplayInfo = true)
        {
            // Выбор файла по умолчанию, если параметр не указан
            var fileName = string.IsNullOrWhiteSpace(parameters) ? DefaultTxtFileName : parameters;

            // Определение типа команды в зависимости от расширения файла
            ICommand commandToExecute;

            if (fileName.EndsWith(".bin", StringComparison.OrdinalIgnoreCase))
            {
                commandToExecute = new CommandBinLoadData(_shapeCollection); // Загрузка данных из бинарного файла
            }
            else // По умолчанию загружаем из текстового файла
            {
                commandToExecute = new CommandTxtLoadData(_shapeCollection, _app); // Загрузка данных из текстового файла
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
