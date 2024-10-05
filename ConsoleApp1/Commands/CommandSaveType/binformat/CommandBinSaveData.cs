using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Команда для сохранения данных о фигурах в бинарный файл.
/// </summary>
internal class CommandBinSaveData : ICommand
{
    private readonly ShapeCollection _shapeCollection;
    private List<byte> data = new List<byte>();
    private const string DefaultFileName = "ShapeData.bin";

    /// <summary>
    /// Имя команды, которое используется для ее вызова.
    /// </summary>
    public string Name => "сохранить_данные";

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="CommandBinSaveData"/>.
    /// </summary>
    /// <param name="shapeCollection">Коллекция фигур для сохранения.</param>
    /// <exception cref="ArgumentNullException">Выбрасывается, если передана пустая коллекция.</exception>
    public CommandBinSaveData(ShapeCollection shapeCollection)
    {
        _shapeCollection = shapeCollection ?? throw new ArgumentNullException(nameof(shapeCollection));
    }

    /// <summary>
    /// Выполняет сохранение коллекции фигур в бинарный файл.
    /// </summary>
    /// <param name="parameters">Имя файла для сохранения данных.</param>
    /// <param name="shouldDisplayInfo">Флаг для вывода информации о результате сохранения.</param>
    public void Execute(string parameters, bool shouldDisplayInfo = true)
    {
        var fileName = string.IsNullOrWhiteSpace(parameters) ? DefaultFileName : parameters;

        try
        {
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                var fullData = new List<byte>();

                foreach (var shape in _shapeCollection)
                {
                    byte[] shapeData;

                    switch (shape)
                    {
                        case Circle circle:
                            shapeData = CreateCircleData(circle);
                            break;
                        case Square square:
                            shapeData = CreateSquareData(square);
                            break;
                        case Triangle triangle:
                            shapeData = CreateTriangleData(triangle);
                            break;
                        case Rectangle rectangle:
                            shapeData = CreateRectangleData(rectangle);
                            break;
                        case Polygon polygon:
                            shapeData = CreatePolygonData(polygon);
                            break;
                        default:
                            throw new InvalidOperationException("Неизвестная фигура");
                    }

                    // Добавляем байты фигуры в общий массив
                    fullData.AddRange(shapeData);
                }

                // Записываем весь массив байтов в файл
                stream.Write(fullData.ToArray(), 0, fullData.Count);
            }

            if (shouldDisplayInfo)
            {
                Console.WriteLine($"Фигуры успешно сохранены в файл '{fileName}'.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
        }
    }

    /// <summary>
    /// Создает массив байтов для круга.
    /// </summary>
    /// <param name="circle">Круг для сохранения.</param>
    /// <returns>Массив байтов, представляющий данные круга.</returns>
    private byte[] CreateCircleData(Circle circle)
    {
        data.Clear();
        data.Add(1); // Уникальный идентификатор для круга
        data.AddRange(BitConverter.GetBytes(circle.Radius));
        return data.ToArray();
    }

    /// <summary>
    /// Создает массив байтов для квадрата.
    /// </summary>
    /// <param name="square">Квадрат для сохранения.</param>
    /// <returns>Массив байтов, представляющий данные квадрата.</returns>
    private byte[] CreateSquareData(Square square)
    {
        data.Clear();
        data.Add(2); // Уникальный идентификатор для квадрата
        data.AddRange(BitConverter.GetBytes(square.A));
        return data.ToArray();
    }

    /// <summary>
    /// Создает массив байтов для треугольника.
    /// </summary>
    /// <param name="triangle">Треугольник для сохранения.</param>
    /// <returns>Массив байтов, представляющий данные треугольника.</returns>
    private byte[] CreateTriangleData(Triangle triangle)
    {
        data.Clear();
        data.Add(3); // Уникальный идентификатор для треугольника
        data.AddRange(BitConverter.GetBytes(triangle.A));
        data.AddRange(BitConverter.GetBytes(triangle.B));
        data.AddRange(BitConverter.GetBytes(triangle.C));
        return data.ToArray();
    }

    /// <summary>
    /// Создает массив байтов для прямоугольника.
    /// </summary>
    /// <param name="rectangle">Прямоугольник для сохранения.</param>
    /// <returns>Массив байтов, представляющий данные прямоугольника.</returns>
    private byte[] CreateRectangleData(Rectangle rectangle)
    {
        data.Clear();
        data.Add(4); // Уникальный идентификатор для прямоугольника
        data.AddRange(BitConverter.GetBytes(rectangle.Width));
        data.AddRange(BitConverter.GetBytes(rectangle.Height));
        return data.ToArray();
    }

    /// <summary>
    /// Создает массив байтов для многоугольника.
    /// </summary>
    /// <param name="polygon">Многоугольник для сохранения.</param>
    /// <returns>Массив байтов, представляющий данные многоугольника.</returns>
    private byte[] CreatePolygonData(Polygon polygon)
    {
        data.Clear();
        data.Add(5); // Уникальный идентификатор для многоугольника
        data.AddRange(BitConverter.GetBytes(polygon.Points.Count));

        foreach (var point in polygon.Points)
        {
            data.AddRange(BitConverter.GetBytes(point.X));
            data.AddRange(BitConverter.GetBytes(point.Y));
        }

        return data.ToArray();
    }

    /// <summary>
    /// Возвращает строку с информацией о команде и примере использования.
    /// </summary>
    public string Help() =>
        "Команда 'сохранить_данные' сохраняет фигуры в бинарный файл.\n" +
        "Пример: сохранить_данные имя_файла.bin";
}
