﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Infrastructure;

namespace ConsoleApp1.Commands.CommandSaveType.txt
{
    internal class CommandTxtSaveData : ICommand
    {
        private readonly IShapeCollection _shapeCollection;
        private const string DefaultFileName = "ShapeData.txt"; // Имя файла по умолчанию

        /// <summary>
        /// Получает имя команды.
        /// </summary>
        /// <value>Имя команды, используемое для её идентификации. В данном случае — "сохранить_данные".</value>
        public string Name => "сохранить_данные";

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CommandSaveData"/>.
        /// </summary>
        /// <param name="shapeCollection">Коллекция фигур, данные из которой будут сохранены в файл.</param>
        public CommandTxtSaveData(IShapeCollection shapeCollection)
        {
            _shapeCollection = shapeCollection;
        }

        /// <summary>
        /// Выполняет команду, сохраняя данные о фигурах из коллекции в указанный файл.
        /// Если имя файла не указано, используется значение по умолчанию "ShapeData.txt".
        /// </summary>
        /// <param name="parameters">Имя файла, в который будут сохранены данные. Если параметр пустой, используется значение по умолчанию.</param>
        public void Execute(string parameters, bool shouldDisplayInfo = true)
        {
            // Используем имя файла по умолчанию, если параметр пустой
            var fileName = string.IsNullOrWhiteSpace(parameters) ? DefaultFileName : parameters;

            try
            {
                var shapes = _shapeCollection.ToList();

                // Открываем файл в режиме добавления (append)
                using (var writer = new StreamWriter(fileName, true, Encoding.UTF8))
                {
                    shapes
                   .Select(shape => shape.GetCommand())
                   .ToList()
                   .ForEach(writer.WriteLine);
                }

                Console.WriteLine($"Данные успешно сохранены в файл '{fileName}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
            }
        }
        /// <summary>
        /// Получает описание команды и её использования.
        /// </summary>
        /// <returns>Описание команды.</returns>
        public string Help()
        {
            return "Команда 'сохранить_данные' сохраняет данные о фигурах в текстовый файл.\n" +
                   "Параметры команды: имя файла для сохранения. Если имя файла не указано, используется значение по умолчанию 'ShapeData.txt'.\n" +
                   "Пример использования:\n" +
                   "сохранить_данные имя_файла.txt\n" +
                   "или\n" +
                   "сохранить_данные\n";
        }
    }
}
