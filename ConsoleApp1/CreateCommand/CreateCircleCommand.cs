using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1.GeometricShapeCalculator.Infrastructure
{
    public class CreateCircleCommand : IApp
    {
        public string Name => "добавить_круг";

        public void Execute( App app, string parameters = "")
        {
            double radius = ParseRadius(parameters);
            var circle = new Circle(radius); // Создаем круг с заданным радиусом

            double area = circle.GetArea();
            double circumference = circle.GetPerimeter();

            Console.WriteLine($"Площадь круга: {area}");
            Console.WriteLine($"Длина окружности: {circumference}");

            app.Add(circle); 
        }
        private double ParseRadius(string parameters)
        {
            // Регулярное выражение для извлечения радиуса из строки в формате [x]
            var pattern = @"\[(.*?)\]";
            var match = Regex.Match(parameters, pattern);

            if (match.Success)
            {
                var radiusStr = match.Groups[1].Value;
                if (double.TryParse(radiusStr, NumberStyles.Float, CultureInfo.InvariantCulture, out double radius) && radius > 0)
                {
                    return radius;
                }
                else
                {
                    throw new ArgumentException("Некорректный радиус. Пожалуйста, введите положительное число.");
                }
            }
            else
            {
                throw new ArgumentException("Некорректный формат данных. Пожалуйста, используйте формат [x], где x — радиус круга.");
            }
        }

    }
}
