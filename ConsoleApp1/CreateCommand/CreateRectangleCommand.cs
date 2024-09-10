using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1.GeometricShapeCalculator.Infrastructure
{
    internal class CreateRectangleCommand : IApp
    {
        public string Name => "добавить_прямоугольник";

        public void Execute(App app, string parameters = "")
        {
            // Разделяем параметры на ширину и высоту
            var dimensions = ParseDimensions(parameters);
            var rectangle = new Rectangle(dimensions.Width, dimensions.Height); // Создаем прямоугольник

            double area = rectangle.GetArea();
            double perimeter = rectangle.GetPerimeter();

            Console.WriteLine($"Площадь прямоугольника: {area}");
            Console.WriteLine($"Периметр прямоугольника: {perimeter}");

            app.Add(rectangle); // Добавляем прямоугольник в список фигур
        }
        private (double Width, double Height) ParseDimensions(string parameters)
        {
            // Регулярное выражение для извлечения ширины и высоты из строки в формате [ширина;высота]
            var pattern = @"\[(\d+(\.\d+)?);(\d+(\.\d+)?)\]";
            var match = Regex.Match(parameters, pattern);

            if (match.Success)
            {
                var widthStr = match.Groups[1].Value;
                var heightStr = match.Groups[3].Value;

                if (double.TryParse(widthStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double width) &&
                    double.TryParse(heightStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double height) &&
                    width > 0 && height > 0)
                {
                    return (width, height);
                }
                else
                {
                    throw new ArgumentException("Некорректные размеры. Пожалуйста, введите положительные числа для ширины и высоты.");
                }
            }
            else
            {
                throw new ArgumentException("Некорректный формат данных. Пожалуйста, используйте формат [ширина;высота], где ширина и высота — положительные числа.");
            }
        }
    }
}
