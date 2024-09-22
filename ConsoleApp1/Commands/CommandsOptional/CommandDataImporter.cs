using ConsoleApp1.GeometricShapeCalculator.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Commands
{
    /// <summary>
    /// Класс для импорта данных о фигурах из строкового представления и добавления их в коллекцию фигур.
    /// </summary>
    internal class CommandDataImporter
    {
        private readonly ShapeCollection _shapeCollection;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CommandDataImporter"/> с указанной коллекцией фигур.
        /// </summary>
        /// <param name="shapeCollection">Коллекция фигур, в которую будут добавляться созданные фигуры.</param>
        public CommandDataImporter(ShapeCollection shapeCollection)
        {
            _shapeCollection = shapeCollection;
        }

        /// <summary>
        /// Парсит строку с информацией о фигуре и добавляет созданную фигуру в коллекцию.
        /// </summary>
        /// <param name="line">Строка, содержащая описание фигуры. Формат строки должен соответствовать определенному шаблону для каждой фигуры.</param>
        /// <remarks>
        /// Метод пытается определить тип фигуры из строки и вызывает соответствующий метод для создания фигуры.
        /// Если тип фигуры неизвестен или возникает ошибка при разборе строки, выводится сообщение об ошибке.
        /// </remarks>
        public void ParseAndAddShape(string line)
        {
            var parts = line.Split(',')
                .Select(part => part.Trim())
                .ToArray();

            if (parts.Length < 2)
            {
                Console.WriteLine($"Невный формат строки: {line}");
                return;
            }

            var shapeType = parts[0].Split(':').Last().Trim();

            try
            {
                Shape shape;

                switch (shapeType)
                {
                    case "Circle":
                        shape = CommandCreateCircle.FromString(line);
                        break;
                    case "Square":
                        shape = CommandCreateSquare.FromString(line);
                        break;
                    case "Rectangle":
                        shape = CommandCreateRectangle.FromString(line);
                        break;
                    case "Triangle":
                        shape = CommandCreateTriangle.FromString(line);
                        break;
                    case "Polygon":
                        shape = CommandCreatePolygon.FromString(line);
                        break;
                    default:
                        throw new FormatException($"Неизвестная фигура: {shapeType}");
                }

                _shapeCollection.Add(shape);
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Ошибка при разборе {shapeType}: {ex.Message}");
            }
        }
        
    }
}
