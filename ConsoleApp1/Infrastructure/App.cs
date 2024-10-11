using ConsoleApp1.Commands;
using ConsoleApp1.Commands.CommandSaveType;
using ConsoleApp1.CommandsToAddShapes;
using ConsoleApp1.Extensions;
using ConsoleApp1.GeometricShapeCalculator.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
            commands = new List<ICommand>(); // Используем поле класса, а не создаем новую локальную переменную
            var types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var type in types.Where(t => typeof(ICommand).IsAssignableFrom(t) && !t.IsAbstract))
            {
                ICommand instance = null;

                // Если у класса есть конструктор с ShapeCollection
                if (type.GetConstructor(new[] { typeof(ShapeCollection) }) != null)
                {
                    instance = Activator.CreateInstance(type, ShapeCollection) as ICommand;
                }
                // Если у класса есть конструктор без параметров
                else if(type.GetConstructor(Type.EmptyTypes) != null)
                {
                    instance = Activator.CreateInstance(type) as ICommand;
                }

                if (instance != null)
                {
                    commands.Add(instance);
                }
            }
            // Добавляем команду помощи с текущим списком команд
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
