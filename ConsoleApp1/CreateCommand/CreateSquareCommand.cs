using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1.GeometricShapeCalculator.Infrastructure
{
    internal class CreateSquareCommand : IApp
    {
        public string Name => "добавить_квадрат";

        public void Execute(App app, string parameters = "")
        {

            double a = ParseSide(parameters);
            var square = new Square(a);

            double area = square.GetArea();
            double perimeter = square.GetPerimeter();

            Console.WriteLine($"Площадь квадрата: {area}");
            Console.WriteLine($"Периметр квадрата: {perimeter}");

            app.Add(square); // Добавляем квадрат в список фигур
        }
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
    }
    
}
