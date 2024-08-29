using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Triangle : Shape
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }

        public Triangle(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        public override double CalculateArea()
        {
            double s = CalculatePerimetr() / 2;
            return Math.Sqrt(s * (s - A) * (s - B) * (s - C));
        }

        public override double CalculatePerimetr() => A + B + C;

        public override void Display()
        {
            Console.WriteLine($"Треугольник: Сторона A = {A}, Сторона B = {B}, Сторона C = {C}");
            Console.WriteLine($"Площадь = {CalculateArea():F2}");
            Console.WriteLine($"Периметр = {CalculatePerimetr():F2}");
        }


        public override string GetInputData()
        {
            return $"Треугольник: Сторона A = {A}, Сторона B = {B}, Сторона C = {C}";
        }
    }
}
