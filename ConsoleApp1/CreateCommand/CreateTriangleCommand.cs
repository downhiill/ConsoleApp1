using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1.GeometricShapeCalculator.Infrastructure
{
    internal class CreateTriangleCommand : IApp
    {
        public string Name => "добавить_треугольник";

        public void Execute(App app, string parameters = "")
        {
            // Извлекаем параметры треугольника из строки в формате [2;4;3]
            var sides = ParseSides(parameters);

            if (sides.Length == 3 &&
                double.TryParse(sides[0], NumberStyles.Float, CultureInfo.InvariantCulture, out double a) &&
                double.TryParse(sides[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double b) &&
                double.TryParse(sides[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double c) &&
                a > 0 && b > 0 && c > 0)
            {
                var triangle = new Triangle(a, b, c);

                double area = triangle.GetArea();
                double perimeter = triangle.GetPerimeter();

                Console.WriteLine($"Площадь треугольника: {area}");
                Console.WriteLine($"Периметр треугольника: {perimeter}");

                app.Add(triangle); // Добавляем треугольник в список фигур
            }
            else
            {
                Console.WriteLine("Некорректные параметры. Пожалуйста, введите положительные числа для сторон треугольника в формате [сторона1;сторона2;сторона3].");
            }
        }
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
    
    }
}
