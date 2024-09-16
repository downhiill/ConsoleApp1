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
    /// Команда для создания и добавления треугольника в коллекцию.
    /// </summary>
    internal class CommandCreateTriangle : ICommand
    {
        private readonly ShapeCollection _shapeCollection;

        public CommandCreateTriangle(ShapeCollection shapeCollection)
        {
            _shapeCollection = shapeCollection;
        }
        /// <summary>
        /// Получает имя команды.
        /// </summary>
        /// <value>Имя команды, используемое для её идентификации. В данном случае — "добавить_треугольник".</value>
        public string Name => "добавить_треугольник";

        /// <summary>
        /// Выполняет команду, создавая треугольник с заданными сторонами и добавляя его в коллекцию фигур.
        /// </summary>
        /// <param name="parameters">Строка параметров, содержащая стороны треугольника в формате [сторона1; сторона2; сторона3].</param>
        public void Execute(string parameters)
        {
            // Извлекаем параметры треугольника из строки в формате [1;2;3]
            var sides = ParseSides(parameters);

            if (sides.Length == 3 &&
                double.TryParse(sides[0], NumberStyles.Float, CultureInfo.InvariantCulture, out double a) &&
                double.TryParse(sides[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double b) &&
                double.TryParse(sides[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double c) &&
                a > 0 && b > 0 && c > 0)
            {
                var triangle = new Triangle(a, b, c);

                Console.WriteLine($"Площадь треугольника: {triangle.S()}");
                Console.WriteLine($"Периметр треугольника: {triangle.P()}");

                _shapeCollection.Add(triangle); // Добавляем треугольник в список фигур
            }
            else
            {
                Console.WriteLine("Некорректные параметры. Пожалуйста, введите положительные числа для сторон треугольника в формате [сторона1;сторона2;сторона3].");
            }
        }

        /// <summary>
        /// Парсит строки с параметрами сторон треугольника из строки формата [сторона1;сторона2;сторона3].
        /// </summary>
        /// <param name="parameters">Строка параметров, содержащая стороны треугольника в формате [сторона1;сторона2;сторона3].</param>
        /// <returns>Массив строк, представляющий стороны треугольника.</returns>
        /// <exception cref="ArgumentException">Выбрасывается, если формат строки некорректен или количество параметров не равно трем.</exception>
        private string[] ParseSides(string parameters)
        {
            var pattern = @"\[(.*?)\]";
            var match = Regex.Match(parameters, pattern);

            if (match.Success)
            {
                var sidesStr = match.Groups[1].Value;
                var sides = sidesStr.Split(';');

                if (sides.Length == 3)
                {
                    return sides;
                }
                else
                {
                    throw new ArgumentException("Некорректное количество параметров. Пожалуйста, введите три стороны треугольника.");
                }
            }
            else
            {
                throw new ArgumentException("Некорректный формат данных. Пожалуйста, используйте формат [сторона1;сторона2;сторона3].");
            }
        }

        /// <summary>
        /// Получает описание команды и её использования.
        /// </summary>
        /// <returns>Описание команды.</returns>
        public string Help()
        {
            return "Команда 'добавить_треугольник' создает треугольник с заданными сторонами и добавляет его в коллекцию фигур.\n" +
                   "Формат параметров: [сторона1; сторона2; сторона3], где стороны — положительные числа.\n" +
                   "Примеры использования:\n" +
                   "добавить_треугольник [3;4;5]\n" +
                   "добавить_треугольник [6.5;7.8;9.1]\n";
        }

    }
}
