﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1.GeometricShapeCalculator.Infrastructure
{
    /// <summary>
    /// Команда для создания и добавления квадрата в коллекцию.
    /// </summary>
    internal class CommandCreateSquare : ICommand
    {
        private readonly ShapeCollection _shapeCollection;

        public CommandCreateSquare(ShapeCollection shapeCollection)
        {
            _shapeCollection = shapeCollection;
        }
        /// <summary>
        /// Получает имя команды.
        /// </summary>
        /// <value>Имя команды, используемое для её идентификации. В данном случае — "добавить_квадрат".</value>
        public string Name => "добавить_квадрат";

        /// <summary>
        /// Выполняет команду, создавая квадрат с заданной длиной стороны и добавляя его в коллекцию фигур.
        /// </summary>
        /// <param name="parameters">Строка параметров, содержащая длину стороны квадрата в формате [длина_стороны].</param>
        public void Execute(string parameters)
        {

            double a = ParseSide(parameters);
            var square = new Square(a);

            Console.WriteLine($"Площадь квадрата: {square.S()}");
            Console.WriteLine($"Периметр квадрата: {square.P()}");

            _shapeCollection.Add(square); // Добавляем квадрат в список фигур
        }

        /// <summary>
        /// Парсит строку с параметром длины стороны квадрата из строки формата [длина_стороны].
        /// </summary>
        /// <param name="parameters">Строка параметров, содержащая длину стороны квадрата в формате [длина_стороны].</param>
        /// <returns>Длину стороны квадрата.</returns>
        /// <exception cref="ArgumentException">Выбрасывается, если формат строки некорректен или длина стороны некорректна.</exception>
        private double ParseSide(string parameters)
        {
            // Регулярное выражение для извлечения длины стороны из строки в формате [длина_стороны]
            var pattern = @"\[(.*?)\]";
            var match = Regex.Match(parameters, pattern);

            if (match.Success)
            {
                var sideLengthStr = match.Groups[1].Value;
                if (double.TryParse(sideLengthStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double sideLength) && sideLength > 0)
                {
                    return sideLength;
                }
                else
                {
                    throw new ArgumentException("Некорректная длина стороны. Пожалуйста, введите положительное число.");
                }
            }
            else
            {
                throw new ArgumentException("Некорректный формат данных. Пожалуйста, используйте формат [длина_стороны], где длина стороны — положительное число.");
            }
        }

        /// <summary>
        /// Получает описание команды и её использования.
        /// </summary>
        /// <returns>Описание команды.</returns>
        public string Help()
        {
            return "Команда 'добавить_квадрат' создает квадрат с заданной длиной стороны и добавляет его в коллекцию фигур.\n" +
                   "Формат параметров: [длина_стороны], где длина стороны — положительное число.\n" +
                   "Примеры использования:\n" +
                   "добавить_квадрат [4.5]\n" +
                   "добавить_квадрат [10]\n";
        }
    }
    
}
