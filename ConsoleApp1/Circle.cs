using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle(double radius)
        {
            Radius = radius;
        }

        public override double CalculateArea() => Math.PI * Radius * Radius;

        public override double CalculatePerimetr() => 2 * Math.PI * Radius;

        public override void Display()
        {
            Console.WriteLine($"Круг: Радиус = {Radius}");
            Console.WriteLine($"Площадь = {CalculateArea():F2}");
            Console.WriteLine($"Длина окружности = {CalculatePerimetr():F2}");

        }

        public override string GetInputData()
        {
            return $"Круг: Радиус = {Radius} Площадь = {CalculateArea()} Периметр = {CalculatePerimetr()}";
        }
    }
}
