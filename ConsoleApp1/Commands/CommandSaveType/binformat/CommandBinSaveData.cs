using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Команда для сохранения данных о фигурах в бинарный файл.
/// </summary>
internal class CommandBinSaveData : ICommand
{
    private readonly ShapeCollection _shapeCollection; // Коллекция фигур для сохранения
    private const string DefaultFileName = "ShapeData.bin"; // Имя файла по умолчанию

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
        _shapeCollection = shapeCollection ?? throw new ArgumentNullException(nameof(shapeCollection), "Коллекция фигур не может быть null.");
    }

    /// <summary>
    /// Выполняет сохранение коллекции фигур в бинарный файл.
    /// </summary>
    /// <param name="parameters">Имя файла для сохранения данных. Если не указано, используется значение по умолчанию.</param>
    /// <param name="shouldDisplayInfo">Флаг для вывода информации о результате сохранения.</param>
    public void Execute(string parameters, bool shouldDisplayInfo = true)
    {
        var fileName = string.IsNullOrWhiteSpace(parameters) ? DefaultFileName : parameters;

        try
        {
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                // Используем LINQ для получения всех байтовых данных фигур
                var fullData = _shapeCollection
                    .Select(shape => shape.SaveToBinary())
                    .SelectMany(shapeData => shapeData) // Разворачиваем массивы байтов
                    .ToList();

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
    /// Возвращает строку с информацией о команде и примере использования.
    /// </summary>
    public string Help() =>
        "Команда 'сохранить_данные' сохраняет фигуры в бинарный файл.\n" +
        "Пример: сохранить_данные имя_файла.bin";
}
