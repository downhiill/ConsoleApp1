using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1.Commands.CommandSaveType
{
    /// <summary>
    /// Команда для загрузки данных о фигурах из бинарного файла.
    /// </summary>
    internal class CommandBinLoadData : ICommand
    {
        private readonly ShapeCollection _shapeCollection; // Коллекция фигур для загрузки данных
        private const string DefaultFileName = "ShapeData.bin"; // Имя файла по умолчанию

        /// <summary>
        /// Получает имя команды.
        /// </summary>
        public string Name => "загрузить_данные";

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CommandBinLoadData"/>.
        /// </summary>
        /// <param name="shapeCollection">Коллекция фигур, в которую будут загружены данные из файла.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если коллекция фигур равна null.</exception>
        public CommandBinLoadData(ShapeCollection shapeCollection)
        {
            _shapeCollection = shapeCollection ?? throw new ArgumentNullException(nameof(shapeCollection), "Коллекция фигур не может быть null");
        }

        /// <summary>
        /// Выполняет команду, загружая данные о фигурах из указанного файла.
        /// Если имя файла не указано, используется значение по умолчанию "ShapeData.bin".
        /// </summary>
        /// <param name="parameters">Имя файла, из которого будут загружены данные. Если параметр пустой, используется значение по умолчанию.</param>
        /// <param name="shouldDisplayInfo">Указывает, нужно ли отображать информацию об успешном выполнении команды.</param>
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
                    // Читаем фигуры из бинарного файла
                    while (fileStream.Position < fileStream.Length)
                    {
                        var shape = ReadShape(fileStream);
                        Console.WriteLine($"Загружена фигура: {shape.GetType().Name}"); // Вывод информации о загруженной фигуре
                        _shapeCollection.Add(shape);
                    }
                }

                if (shouldDisplayInfo)
                {
                    Console.WriteLine("Данные успешно загружены в коллекцию.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Считывает данные фигуры из потока.
        /// </summary>
        /// <param name="stream">Поток с бинарными данными.</param>
        /// <returns>Восстановленная фигура.</returns>
        /// <exception cref="InvalidOperationException">Выбрасывается при неизвестном типе фигуры.</exception>
        private Shape ReadShape(FileStream stream)
        {
            int shapeId = stream.ReadByte();

            Shape shape = CreateShapeById(shapeId);
            shape.LoadFromBinary(stream);
            return shape;
        }

        // Список фабрик для создания фигур
        private static readonly List<Func<Shape>> ShapeFactories = new List<Func<Shape>>
        {
            () => new Circle(),    // 1
            () => new Square(),    // 2
            () => new Triangle(),   // 3
            () => new Rectangle(),  // 4
            () => new Polygon()     // 5
        };

        /// <summary>
        /// Создает фигуру по ее идентификатору.
        /// </summary>
        /// <param name="shapeId">Идентификатор фигуры.</param>
        /// <returns>Созданная фигура.</returns>
        /// <exception cref="InvalidOperationException">Выбрасывается при неизвестном типе фигуры.</exception>
        private Shape CreateShapeById(int shapeId)
        {
            if (shapeId >= 1 && shapeId <= ShapeFactories.Count)
            {
                return ShapeFactories[shapeId - 1](); // -1 для корректного индекса
            }

            throw new InvalidOperationException("Неизвестный тип фигуры");
        }

        /// <summary>
        /// Возвращает описание команды и примеры использования.
        /// </summary>
        /// <returns>Описание команды.</returns>
        public string Help()
        {
            return "Команда 'загрузить_данные' загружает данные о фигурах из бинарного файла.\n" +
                   "Пример: загрузить_данные имя_файла.bin или загрузить_данные (по умолчанию используется 'ShapeData.bin').";
        }
    }
}
