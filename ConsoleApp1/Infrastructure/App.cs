using ConsoleApp1.Commands;
using ConsoleApp1.Commands.CommandSaveType;
using ConsoleApp1.CommandsToAddShapes;
using ConsoleApp1.Extensions;
using ConsoleApp1.GeometricShapeCalculator.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    /// <summary>
    /// Представляет основное приложение для работы с различными фигурами.
    /// </summary>
    public class App
    {
        /// <summary>
        /// Коллекция фигур, используемая в приложении.
        /// </summary>
        public ShapeCollection ShapeCollection { get; } = new ShapeCollection();

        /// <summary>
        /// Список команд, доступных для выполнения в приложении.
        /// </summary>
        public readonly List<ICommand> commands;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="App"/> и настраивает доступные команды.
        /// </summary>
        public App()
        {
            // Инициализация всех команд, доступных в приложении
            commands = new List<ICommand>
            {
                new CommandCreateCircle(ShapeCollection),
                new CommandCreateRectangle(ShapeCollection),
                new CommandCreateTriangle(ShapeCollection),
                new CommandCreateSquare(ShapeCollection),
                new CommandCreatePolygon(ShapeCollection),
                new CommandDisplayTotalArea(ShapeCollection),
                new CommandDisplayTotalPerimetrs(ShapeCollection),
                new CommandBinSaveData(ShapeCollection), // Команда для сохранения данных в бинарный файл
                new CommandBinLoadData(ShapeCollection), // Команда для загрузки данных из бинарного файла
                new CommandExit() // Команда для выхода из приложения
            };

            // Добавляем команду для отображения списка команд (помощь)
            commands.Add(new CommandHelp(commands));
        }

        /// <summary>
        /// Обрабатывает выбор команды пользователя и выполняет соответствующие действия.
        /// </summary>
        public void Run()
        {
            while (true)
            {
                // Получение команды и её параметров от пользователя
                var (commandKey, parameters) = CommandsParser.GetCommandAndParameters();
                ExecuteCommand(commandKey, parameters);
            }
        }

        /// <summary>
        /// Выполняет команду на основе переданного ключа команды.
        /// </summary>
        /// <param name="commandKey">Ключ команды (название).</param>
        /// <param name="parameters">Параметры, переданные команде.</param>
        public void ExecuteCommand(string commandKey, string parameters)
        {
            // Поиск команды по её имени
            var command = commands.FirstOrDefault(c => c.Name == commandKey);

            if (command != null)
            {
                // Попытка выполнить команду с переданными параметрами
                CommandExtensions.TryExecute(command, parameters);
            }
            else
            {
                Console.WriteLine("Неизвестная команда. Введите 'помощь' для списка команд.");
            }
        }
    }
}
