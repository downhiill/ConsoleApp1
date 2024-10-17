using ConsoleApp1.Commands;
using ConsoleApp1.Commands.CommandSaveType;
using ConsoleApp1.CommandsToAddShapes;
using ConsoleApp1.Extensions;
using ConsoleApp1.GeometricShapeCalculator.Infrastructure;
using ConsoleApp1.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

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

        private DependencyContainer container;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="App"/> и настраивает доступные команды.
        /// </summary>
        public App()
        {
            container = new DependencyContainer();

            // Регистрируем зависимости
            container.Register<IShapeCollection, ShapeCollection>();

            commands = new List<ICommand>(); 
            var types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var type in types.Where(t => typeof(ICommand).IsAssignableFrom(t) && !t.IsAbstract))
            {
                var instance = CreateCommand(type);

                if (instance != null)
                {
                    commands.Add(instance);
                }
            }
            
            commands.Add(new CommandHelp(commands));
        }

        /// <summary>
        /// Создает экземпляр команды на основе предоставленного типа.
        /// </summary>
        /// <param name="type">Тип команды, для которой необходимо создать экземпляр.</param>
        /// <returns>Экземпляр команды <see cref="ICommand"/>, или null, если не удалось создать экземпляр.</returns>
        private ICommand CreateCommand(Type type)
        {
            // Получаем конструкторы для типа
            var constructor = type.GetConstructors().FirstOrDefault();

            if (constructor == null)
            {
                return null; 
            }

            // Получаем параметры конструктора
            var parameters = constructor.GetParameters();

            // Если нет параметров, создаем экземпляр без параметров
            if (parameters.Length == 0)
            {
                return Activator.CreateInstance(type) as ICommand;
            }

            // Для хранения зависимостей, которые нужно передать в конструктор
            var args = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                var parameterType = parameters[i].ParameterType;

                // Используем контейнер для разрешения зависимости
                try
                {
                    args[i] = container.Resolve(parameterType);
                }
                catch (KeyNotFoundException)
                {
                    return null; // Если зависимость не зарегистрирована, возвращаем null
                }
            }

            // Создаем экземпляр команды с разрешенными зависимостями
            return Activator.CreateInstance(type, args) as ICommand;
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
