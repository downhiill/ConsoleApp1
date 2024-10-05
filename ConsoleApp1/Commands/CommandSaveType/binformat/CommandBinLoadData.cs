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
        private readonly ShapeCollection _shapeCollection;
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
                    while (fileStream.Position < fileStream.Length)
                    {
                        var shapeData = ReadShapeData(fileStream);
                        _shapeCollection.Add(shapeData);
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
        private Shape ReadShapeData(FileStream stream)
        {
            // Чтение уникального идентификатора фигуры
            int shapeId = stream.ReadByte();

            switch (shapeId)
            {
                case 1: return ReadCircle(stream);
                case 2: return ReadSquare(stream);
                case 3: return ReadTriangle(stream);
                case 4: return ReadRectangle(stream);
                case 5: return ReadPolygon(stream);
                default: throw new InvalidOperationException("Неизвестный тип фигуры");
            }
        }

        /// <summary>
        /// Чтение данных круга из бинарного потока.
        /// </summary>
        /// <param name="stream">Поток данных.</param>
        /// <returns>Экземпляр <see cref="Circle"/> с восстановленными данными.</returns>
        private Circle ReadCircle(FileStream stream)
        {
            var radiusBytes = new byte[sizeof(double)];
            stream.Read(radiusBytes, 0, radiusBytes.Length);
            double radius = BitConverter.ToDouble(radiusBytes, 0);
            return new Circle(radius);
        }

        /// <summary>
        /// Чтение данных квадрата из бинарного потока.
        /// </summary>
        /// <param name="stream">Поток данных.</param>
        /// <returns>Экземпляр <see cref="Square"/> с восстановленными данными.</returns>
        private Square ReadSquare(FileStream stream)
        {
            var sideBytes = new byte[sizeof(double)];
            stream.Read(sideBytes, 0, sideBytes.Length);
            double side = BitConverter.ToDouble(sideBytes, 0);
            return new Square(side);
        }

        /// <summary>
        /// Чтение данных треугольника из бинарного потока.
        /// </summary>
        /// <param name="stream">Поток данных.</param>
        /// <returns>Экземпляр <see cref="Triangle"/> с восстановленными данными.</returns>
        private Triangle ReadTriangle(FileStream stream)
        {
            var sideBytes = new byte[sizeof(double) * 3];
            stream.Read(sideBytes, 0, sideBytes.Length);
            double sideA = BitConverter.ToDouble(sideBytes, 0);
            double sideB = BitConverter.ToDouble(sideBytes, sizeof(double));
            double sideC = BitConverter.ToDouble(sideBytes, sizeof(double) * 2);
            return new Triangle(sideA, sideB, sideC);
        }

        /// <summary>
        /// Чтение данных прямоугольника из бинарного потока.
        /// </summary>
        /// <param name="stream">Поток данных.</param>
        /// <returns>Экземпляр <see cref="Rectangle"/> с восстановленными данными.</returns>
        private Rectangle ReadRectangle(FileStream stream)
        {
            var dataBytes = new byte[sizeof(double) * 2];
            stream.Read(dataBytes, 0, dataBytes.Length);
            double width = BitConverter.ToDouble(dataBytes, 0);
            double height = BitConverter.ToDouble(dataBytes, sizeof(double));
            return new Rectangle(width, height);
        }

        /// <summary>
        /// Чтение данных многоугольника из бинарного потока.
        /// </summary>
        /// <param name="stream">Поток данных.</param>
        /// <returns>Экземпляр <see cref="Polygon"/> с восстановленными данными.</returns>
        private Polygon ReadPolygon(FileStream stream)
        {
            var pointsCountBytes = new byte[sizeof(int)];
            stream.Read(pointsCountBytes, 0, pointsCountBytes.Length);
            int pointsCount = BitConverter.ToInt32(pointsCountBytes, 0);

            var points = new List<Point>();
            for (int i = 0; i < pointsCount; i++)
            {
                var pointData = new byte[sizeof(double) * 2];
                stream.Read(pointData, 0, pointData.Length);
                double x = BitConverter.ToDouble(pointData, 0);
                double y = BitConverter.ToDouble(pointData, sizeof(double));
                points.Add(new Point(x, y));
            }

            return new Polygon(points);
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
