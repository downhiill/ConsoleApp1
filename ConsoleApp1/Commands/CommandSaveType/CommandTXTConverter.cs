using ConsoleApp1.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Commands.CommandSaveType
{
    /// <summary>
    /// Класс, представляющий конвертер для загрузки и сохранения данных о фигурах в текстовом формате.
    /// </summary>
    internal class CommandTXTConverter : IFileHandler
    {
        /// <summary>
        /// Загружает фигуры из текстового файла и выполняет соответствующие команды в приложении.
        /// </summary>
        /// <param name="fileName">Имя файла для загрузки фигур.</param>
        /// <param name="app">Экземпляр <see cref="App"/>, в котором будут выполнены команды для добавления фигур.</param>
        /// <exception cref="FileNotFoundException">Выбрасывается, если файл не найден.</exception>
        public void LoadShapes(string fileName, App app)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"Файл {fileName} не найден.");
            }

            var lines = File.ReadAllLines(fileName, Encoding.UTF8);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                // Разделение строки на имя команды и параметры
                var parts = line.Split(new[] { ' ' }, 2);
                var commandName = parts[0];
                var commandParams = parts.Length > 1 ? parts[1] : string.Empty;

                // Выполнение команды
                app.ExecuteCommand(commandName, commandParams);
            }
        }

        /// <summary>
        /// Сохраняет коллекцию фигур в текстовый файл.
        /// </summary>
        /// <param name="fileName">Имя файла для сохранения фигур.</param>
        /// <param name="shapeCollection">Коллекция фигур для сохранения.</param>
        public void SaveShapes(string fileName, ShapeCollection shapeCollection)
        {
            using (var writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                foreach (var shape in shapeCollection.ToList())
                {
                    // Запись команды, соответствующей фигуре
                    writer.WriteLine(shape.GetCommand());
                }
            }
        }
    }
}
