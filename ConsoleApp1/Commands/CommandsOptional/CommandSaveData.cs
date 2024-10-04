using ConsoleApp1.Commands.CommandSaveType.txt;
using System;

namespace ConsoleApp1.Commands
{
    /// <summary>
    /// Команда для сохранения данных о фигурах в файл.
    /// </summary>
    internal class CommandSaveData : ICommand
    {
        private readonly ShapeCollection _shapeCollection;
        private const string DefaultTxtFileName = "ShapeData.txt";
        private const string DefaultBinFileName = "ShapeData.bin";

        public string Name => "сохранить_данные";

        public CommandSaveData(ShapeCollection shapeCollection)
        {
            _shapeCollection = shapeCollection;
        }

        public void Execute(string parameters, bool shouldDisplayInfo = true)
        {
            var fileName = string.IsNullOrWhiteSpace(parameters) ? DefaultTxtFileName : parameters;

            ICommand commandToExecute;

            if (fileName.EndsWith(".bin", StringComparison.OrdinalIgnoreCase))
            {
                commandToExecute = new CommandBinSaveData(_shapeCollection);
            }
            else // По умолчанию сохраняем в txt
            {
                commandToExecute = new CommandTxtSaveData(_shapeCollection);
            }

            commandToExecute.Execute(fileName, shouldDisplayInfo);
        }

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
